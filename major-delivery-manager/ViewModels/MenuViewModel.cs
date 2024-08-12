using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace major_delivery_manager.ViewModels
{
    class MenuViewModel : BindableBase
    {
        public MenuViewModel() { }

        public IMainWindowsCodeBehind CodeBehind { get; set; }

        /// <summary>
        /// Переход к MainView
        /// </summary>

        private DelegateCommand loadMainVMCommand;
        public DelegateCommand LoadMainVMCommand
        {
            get
            {
                return loadMainVMCommand ?? (new DelegateCommand(OnLoadMainVM, CanLoadMainVM));
            }
        }
        private bool CanLoadMainVM() => true;
        private void OnLoadMainVM()
        {
            CodeBehind.LoadView(ViewType.MAIN);
        }

        /// <summary>
        /// Переход к CreateRequestView
        /// </summary>

        private DelegateCommand loadCreateVMCommand;
        public DelegateCommand LoadCreateVMCommand
        {
            get
            {
                return loadCreateVMCommand ?? (new DelegateCommand(OnLoadCreateVM, CanLoadCreateVM));
            }
        }
        private bool CanLoadCreateVM() => true;
        private void OnLoadCreateVM()
        {
            CodeBehind.LoadView(ViewType.CREATE);
        }

        /// <summary>
        /// Переход к UpdateRequestView
        /// </summary>

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
            CodeBehind.LoadView(ViewType.UPDATE);
        }

        /// <summary>
        /// Переход к RegistryControlView
        /// </summary>

        private DelegateCommand loadRegistryVMCommand;
        public DelegateCommand LoadRegistryVMCommand
        {
            get
            {
                return loadRegistryVMCommand ?? (new DelegateCommand(OnLoadRegistryVM, CanLoadRegistryVM));
            }
        }
        private bool CanLoadRegistryVM() => true;
        private void OnLoadRegistryVM()
        {
            CodeBehind.LoadView(ViewType.REGISTRY);
        }
    }
}
