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

namespace major_delivery_manager.ViewModels
{
    public class CreateRequestViewModel : BindableBase
    {
        private IMainWindowsCodeBehind mainCodeBehind;

        public CreateRequestViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind) + " ERROR!");

            mainCodeBehind = codeBehind;
        }
    }
}
