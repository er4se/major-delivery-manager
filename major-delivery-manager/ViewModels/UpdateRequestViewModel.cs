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

        private DelegateCommand updateRequestCommand;
        public DelegateCommand UpdateRequestCommand
        {
            get
            {
                return updateRequestCommand ?? (new DelegateCommand(OnUpdateRequest, CanUpdateRequest));
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

        public UpdateRequestViewModel(RequestModel request)
        {
            this.request = request;
            requestRepo = new RequestRepository();
            courierRepo = new CourierRepository();

            Couriers = new ObservableCollection<CourierModel>(courierRepo.GetAll());
        }

        private bool CanUpdateRequest() => true;

        private async void OnUpdateRequest()
        {
            bool flag = true;
            foreach (PropertyInfo prop in Request.GetType().GetProperties())
            {
                var value = prop.GetValue(Request);

                if (prop.PropertyType.Name == nameof(String))
                    if (string.IsNullOrEmpty((string)value)) flag = false;
            }

            if (!flag)
            {
                MessageBox.Show("Не все поля заполнены!");
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

        public void Clear()
        {
            var temp = Request.Id;
            Request = new RequestModel();
            Request.Id = temp;
        }
    }
}
