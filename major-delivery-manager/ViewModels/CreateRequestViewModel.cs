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

namespace major_delivery_manager.ViewModels
{
    public class CreateRequestViewModel : BindableBase
    {
        private IMainWindowsCodeBehind mainCodeBehind;
        private RequestModel newRequest;
        private RequestRepository repository;

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

        public string ToCountry
        {
            get => newRequest.ToCountry; 
            set
            {
                newRequest.ToCountry = value;
                RaisePropertyChanged(nameof(ToCountry));
            }
        }

        public string FromCountry
        {
            get => newRequest.FromCountry;
            set
            {
                newRequest.FromCountry = value;
                RaisePropertyChanged(nameof(FromCountry));
            }
        }

        public string ToSattlement
        {
            get => newRequest.ToSattlement;
            set
            {
                newRequest.ToSattlement = value;
                RaisePropertyChanged(nameof(ToSattlement));
            }
        }

        public string FromSattlement
        {
            get => newRequest.FromSattlement;
            set
            {
                newRequest.FromSattlement = value;
                RaisePropertyChanged(nameof(FromSattlement));
            }
        }

        public string Weight
        {
            get => newRequest.Weight.ToString();
            set
            {
                try
                {
                    newRequest.Weight = Convert.ToInt32(value);
                    RaisePropertyChanged(nameof(Weight));
                }
                catch
                {
                    if (value != string.Empty)
                        MessageBox.Show("Неверный формат");
                }
                
            }
        }

        public string Volume
        {
            get => newRequest.Volume.ToString();
            set
            {
                try
                {
                    newRequest.Volume = Convert.ToInt32(value);
                    RaisePropertyChanged(nameof(Volume));
                }
                catch
                {
                    if (value != string.Empty)
                        MessageBox.Show("Неверный формат");
                }
            }
        }

        public string Cost
        {
            get => newRequest.Cost.ToString();
            set
            {
                try
                {
                    newRequest.Cost = Convert.ToInt32(value);
                    RaisePropertyChanged(nameof(Cost));
                }
                catch
                {
                    if (value != string.Empty)
                        MessageBox.Show("Неверный формат");
                }

            }
        }

        public string Amount
        {
            get => newRequest.Amount.ToString();
            set
            {
                try
                {
                    newRequest.Amount = Convert.ToInt32(value);
                    RaisePropertyChanged(nameof(Amount));
                }
                catch
                {
                    if (value != string.Empty)
                        MessageBox.Show("Неверный формат");
                }

            }
        }

        public CreateRequestViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind) + " ERROR!");

            mainCodeBehind = codeBehind;

            newRequest = new RequestModel();
            repository = new RequestRepository();
        }

        private bool CanCreateRequest() => true;

        private void OnCreateRequest()
        {
            if ((ToCountry == string.Empty)
                    || (ToSattlement == string.Empty)
                    || (FromCountry == string.Empty)
                    || (FromSattlement == string.Empty)
                    || (Weight == "0")
                    || (Volume == "0")
                    || (Cost == "0")
                    || (Amount == "0"))
            {
                MessageBox.Show("Не все поля заполнены!");
            }

            else
            {
                try
                {
                    repository.Create(newRequest);
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
            ToCountry       = string.Empty;
            ToSattlement    = string.Empty;
            FromCountry     = string.Empty;
            FromSattlement  = string.Empty;
            Weight          = "0";
            Volume          = "0";
            Cost            = "0";
            Amount          = "0";
        }
    }
}
