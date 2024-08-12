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
        private IRepository<RequestModel> repository;

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
            repository = new RequestRepository();

            if(request != null)
                Request = request;
            else throw new ArgumentNullException(nameof(request));
        }

        private async void OnCancelRequest()
        {
            if ((!string.IsNullOrEmpty(Request.CancelComment)) || Request.CancelComment != "NOTCANCELLED")
            {
                await repository.Update(Request);
                MessageBox.Show("Заявка отменена!");
            }
            else
            {
                MessageBox.Show("Введите комментарий!");
            }
        }
    }
}
