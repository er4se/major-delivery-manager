using major_delivery_manager.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using major_delivery_manager.AppDbContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using Prism.Commands;
using System.Windows;
using System.Reflection;
using System.Configuration;
using major_delivery_manager.Interfaces;

namespace major_delivery_manager.ViewModels
{
    internal class CreateRequestViewModel : BindableBase
    {
        private IMainWindowsCodeBehind mainCodeBehind;
        
        private IRepository<RequestModel> repository;

        private DelegateCommand createRequestCommand;
        public DelegateCommand CreateRequestCommand
        {
            get
            {
                return createRequestCommand ?? (new DelegateCommand(OnCreateRequest, CanCreateRequest));
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

        public CreateRequestViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind) + " ERROR!");

            mainCodeBehind = codeBehind;

            Request = new RequestModel();
            repository = new RequestRepository();
        }

        private bool CanCreateRequest() => true;

        private async void OnCreateRequest()
        {
            bool flag = true;
            foreach(PropertyInfo prop in Request.GetType().GetProperties())
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
                    await repository.Create(Request);
                    Clear();
                
                    MessageBox.Show("Успех!");
                } 
                catch(Exception ex)
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

        public void Clear()
        {
            Request = new RequestModel();
        }
    }
}
