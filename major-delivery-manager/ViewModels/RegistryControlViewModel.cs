using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using major_delivery_manager.Views;
using major_delivery_manager.Interfaces;
using major_delivery_manager.Models;
using System.Collections.ObjectModel;
using System.Security;
using System.Reflection;
using System.Windows;

//LINQ for searching could be good

namespace major_delivery_manager.ViewModels
{
    internal class RegistryControlViewModel : BindableBase
    {
        private IMainWindowsCodeBehind mainCodeBehind;
        public IMainWindowsCodeBehind CodeBehind { get; set; }

        private IRepository<RequestModel> repository;

        private string searchTerm;
        public string SearchTerm
        {
            get => searchTerm;
            set
            {
                searchTerm = value;
                RaisePropertyChanged(nameof(SearchTerm));
                RaisePropertyChanged(nameof(FilteredRegistry));
            }
        }

        private DelegateCommand<RequestModel> loadUpdateVMCommand;
        public DelegateCommand<RequestModel> LoadUpdateVMCommand
        {
            get
            {
                if (loadUpdateVMCommand == null)
                {
                    loadUpdateVMCommand = new DelegateCommand<RequestModel>(OnLoadUpdateVM);
                }
                return loadUpdateVMCommand;
            }
        }

        private DelegateCommand filterRegistryCommand;
        public DelegateCommand FilterRegistryCommand
        {
            get
            {
                if (filterRegistryCommand == null)
                {
                    filterRegistryCommand = new DelegateCommand(OnFilterRegistry);
                }

                return filterRegistryCommand;
            }
        }

        private DelegateCommand<RequestModel> deleteRequestCommand;
        public DelegateCommand<RequestModel> DeleteRequestCommand
        {
            get
            {
                if (deleteRequestCommand == null)
                {
                    deleteRequestCommand = new DelegateCommand<RequestModel>(OnDeleteRequest);
                }

                return deleteRequestCommand;
            }
        }

        private ObservableCollection<RequestModel> registry;
        public ObservableCollection<RequestModel> Registry
        {
            get => registry;
            set
            {
                registry = value;
                RaisePropertyChanged(nameof(Registry));
            }
        }

        private ObservableCollection<RequestModel> filteredRegistry;
        public ObservableCollection<RequestModel> FilteredRegistry
        {
            get 
            {
                if (string.IsNullOrEmpty(SearchTerm))
                    return Registry;

                return new ObservableCollection<RequestModel>(Registry.Where(request => ContainsSearchTerm(request)));
            }
        }

        public RegistryControlViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

            mainCodeBehind = codeBehind;

            repository = new RequestRepository();
            Registry = new ObservableCollection<RequestModel>(repository.GetAll());
        }

        private void OnLoadUpdateVM(RequestModel request)
        {
            UpdateRequestWindowView instance = new UpdateRequestWindowView();
            instance.DataContext = new UpdateRequestViewModel(request);
            instance.Show();
        }

        private void OnFilterRegistry()
        {
            RaisePropertyChanged(nameof(FilteredRegistry));
        }

        private void OnDeleteRequest(RequestModel request)
        {
            if ((request.GetState() == RequestState.CANCELED) || (request.GetState() == RequestState.DONE))
            {
                repository.Delete(request);
                Registry = new ObservableCollection<RequestModel>(repository.GetAll());
                OnFilterRegistry();
            }
            else
            {
                MessageBox.Show("Возможно удаление только выполненых и отмененных заявок");
            }
        }

        private bool ContainsSearchTerm(RequestModel request)
        {
            foreach(PropertyInfo prop in request.GetType().GetProperties())
            {
                if (prop.GetValue(request)?.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) == true)
                    return true;
            }

            return false;
        }
    }
}
