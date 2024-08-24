using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using major_delivery_manager.Interfaces;
using major_delivery_manager.Models;
using Prism.Commands;
using Prism.Mvvm;

namespace major_delivery_manager.ViewModels
{
    internal class CancelRequestViewModel : BindableBase
    {
        private IRepository<RequestCancellationModel> cancelRepo;
        private IRepository<RequestModel> requestRepo;

        private RequestModel request;
        public RequestModel Request
        {
            get => request;
            set
            {
                request = value;
                RaisePropertyChanged(nameof(Request));
            }
        }

        private RequestCancellationModel cancel;
        public RequestCancellationModel Cancel
        {
            get => cancel;
            set
            {
                cancel = value;
                RaisePropertyChanged(nameof(Cancel));
            }
        }

        private DelegateCommand cancelRequestCommand;
        public DelegateCommand CancelRequestCommand
        {
            get
            {
                return cancelRequestCommand ?? (new DelegateCommand(OnCancelRequest));
            }
        }

        public CancelRequestViewModel(RequestModel request)
        {
            cancelRepo = new CancelRepository();
            requestRepo = new RequestRepository();

            if(request != null)
            {
                Request = request;
                Cancel = Request.CancellationModel;
            }
            else throw new ArgumentNullException(nameof(request));
        }

        private async void OnCancelRequest()
        {
            if (Request.GetState() != RequestState.CANCELED)
            {
                if ((!string.IsNullOrEmpty(Cancel.Comment)))
                {
                    Request.ChangeState(new RequestStateCanceled());

                    await cancelRepo.Update(Cancel);
                    await requestRepo.Update(Request);

                    MessageBox.Show("Заявка отменена!");
                }
                else
                {
                    MessageBox.Show("Введите комментарий!");
                }
            }
            else
            {
                MessageBox.Show("Заявка уже отменена");
            }
        }
    }
}
