using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using major_delivery_manager.Views;

namespace major_delivery_manager.ViewModels
{
    class RegistryControlViewModel : BindableBase
    {
        private IMainWindowsCodeBehind mainCodeBehind;
        public IMainWindowsCodeBehind CodeBehind { get; set; }

        public RegistryControlViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

            mainCodeBehind = codeBehind;
        }

        private DelegateCommand loadUpdateVMCommand;
        public DelegateCommand LoadUpdateVMCommand
        {
            get
            {
                return loadUpdateVMCommand ?? (new DelegateCommand(OnLoadUpdateVM, CanLoadUpdateVM));
            }
        }
        
        private bool CanLoadUpdateVM() => true;
        private void OnLoadUpdateVM()
        {
            UpdateRequestWindowView instance = new UpdateRequestWindowView();
            instance.Show();
        }
    }
}
