using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using major_delivery_manager.Views;
using major_delivery_manager.Models;
using System.Windows;
using major_delivery_manager.Interfaces;
using Prism.Commands;
using System.Reflection;
using System.Collections.ObjectModel;

//THE WORST VM ON EARTH, NO TIME FOR REFACTORING

namespace major_delivery_manager.ViewModels
{
    internal class UpdateRequestViewModel : BindableBase
    {
        private IRepository<RequestModel> requestRepo;
        private IRepository<CourierModel> courierRepo;

        private RequestModel request;
        public RequestModel Request 
        {
            get => request;
            set
            {
                if (Request.GetState() == RequestState.NEW)
                {
                    request = value;
                    RaisePropertyChanged(nameof(Request));
                }
                else 
                    MessageBox.Show("Изменение основных данных возможно только при статусе 'НОВАЯ'");
            }
        }

        private CourierModel courier;
        public CourierModel Courier
        {
            get => courier;
            set
            {
                courier = value;
                RaisePropertyChanged(nameof(Courier));
            }
        }

        private ObservableCollection<CourierModel> couriers;
        public ObservableCollection<CourierModel> Couriers
        {
            get => couriers;
            set
            {
                couriers = value;
                RaisePropertyChanged(nameof(Couriers));
            }
        }

        private DelegateCommand updateRequestCommand;
        public DelegateCommand UpdateRequestCommand
        {
            get
            {
                return updateRequestCommand ?? (new DelegateCommand(OnUpdateRequest));
            }
        }

        private DelegateCommand cancelRequestCommand;
        public DelegateCommand CancelRequestCommand
        {
            get
            {
                return cancelRequestCommand ?? (new DelegateCommand(OnLoadCancelRequest));
            }
        }

        private DelegateCommand loadAddCourier;
        public DelegateCommand LoadAddCourier
        {
            get
            {
                if (loadAddCourier == null)
                {
                    loadAddCourier = new DelegateCommand(OnLoadAddCourier);
                }

                return loadAddCourier;
            }
        }

        private DelegateCommand changeRequestCommand;
        public DelegateCommand ChangeRequestCommand
        {
            get
            {
                return changeRequestCommand ?? (new DelegateCommand(OnChangeRequest, CanChangeRequest));
            }
        }

        private DelegateCommand abortRequestCommand;
        public DelegateCommand AbortRequestCommand
        {
            get
            {
                return abortRequestCommand ?? (new DelegateCommand(OnAbortRequest, CanAbortRequest));
            }
        }

        private DelegateCommand assignCourierCommand;
        public DelegateCommand AssignCourierCommand
        {
            get
            {
                return assignCourierCommand ?? (new DelegateCommand(OnAssignCourier));
            }
        }

        private DelegateCommand refreshCouriersCommand;
        public DelegateCommand RefreshCouriersCommand
        {
            get
            {
                return refreshCouriersCommand ?? (new DelegateCommand(OnRefreshCouriers));
            }
        }

        public UpdateRequestViewModel(RequestModel request)
        {
            this.request = request;
            requestRepo = new RequestRepository();
            courierRepo = new CourierRepository();

            if (request.GetState() == RequestState.NEW)
            {
                Couriers = new ObservableCollection<CourierModel>(courierRepo.GetAll());
            }
            else
            {
                try
                {
                    Couriers = new ObservableCollection<CourierModel>();
                    Couriers.Add(courierRepo.GetById(request.ResponsibleId));
                    Courier = Couriers.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private bool CanChangeRequest() => true;

        private async void OnChangeRequest()
        {
            bool flag = true;

            if (Request.GetState() != RequestState.NEW) flag = false;

            MessageBox.Show(Request.GetState().ToString()); //

            foreach (PropertyInfo prop in Request.GetType().GetProperties())
            {
                var value = prop.GetValue(Request);

                if (prop.PropertyType.Name == nameof(String))
                    if (string.IsNullOrEmpty((string)value))
                    {
                        flag = false;
                        break;
                    }
            }

            if (!flag)
            {
                MessageBox.Show("Неверный статус заявки или данные!");
            }
            else
            {
                try
                {
                    await requestRepo.Update(request);

                    MessageBox.Show("Успех!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private bool CanAbortRequest() => true;

        private void OnAbortRequest()
        {
            Clear();
        }

        private void OnLoadAddCourier()
        {
            AddCourierView instance = new AddCourierView();
            instance.DataContext = new AddCourierViewModel();
            instance.Show();
        }

        private void OnLoadCancelRequest()
        {
            if (Courier != null)
            {
                CancelRequestView instance = new CancelRequestView();
                instance.DataContext = new CancelRequestViewModel(Request);
                instance.Show();

                Courier.CancelCommand(Request);
                requestRepo.Update(Request);
            }
        }

        private void OnAssignCourier()
        {
            if (Request.GetState() == RequestState.NEW)
            {
                Request.AssignCourier(Courier);
                Courier.ExecuteCommand(Request);
                requestRepo.Update(Request);

                MessageBox.Show("Успех!");
            }
            else
            {
                MessageBox.Show("Изменение данных о курьере возможно только при статусе 'НОВАЯ'");
            }
        }

        private void OnUpdateRequest()
        {
            if (Courier != null)
            {
                if (Request.GetState() == RequestState.NEW)
                {
                    Request.AssignCourier(Courier);
                    requestRepo.Update(Request);
                }

                Courier.ExecuteCommand(Request);
                requestRepo.Update(Request);
            }
        }

        private void OnRefreshCouriers()
        {
            if (request.GetState() == RequestState.NEW)
            {
                Couriers = new ObservableCollection<CourierModel>(courierRepo.GetAll());
            }
            else
            {
                try
                {
                    Couriers = new ObservableCollection<CourierModel>();
                    Couriers.Add(courierRepo.GetById(request.ResponsibleId));
                    Courier = Couriers.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void Clear()
        {
            var temp = Request.Id;
            Request = new RequestModel();
            Request.Id = temp;
        }
    }
}
