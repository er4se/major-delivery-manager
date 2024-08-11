using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using major_delivery_manager.Views;
using major_delivery_manager.Models;
using System.Windows;

namespace major_delivery_manager.ViewModels
{
    internal class UpdateRequestViewModel : BindableBase
    {
        //private IMainWindowsCodeBehind mainCodeBehind;
        //
        //public UpdateRequestViewModel(IMainWindowsCodeBehind codeBehind)
        //{
        //    if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));
        //
        //    mainCodeBehind = codeBehind;
        //}

        //public UpdateRequestViewModel()
        //{
        //
        //}

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

        public UpdateRequestViewModel(RequestModel request)
        {
            Request = request;
            MessageBox.Show(Request.FromSettlement);
        }
    }
}
