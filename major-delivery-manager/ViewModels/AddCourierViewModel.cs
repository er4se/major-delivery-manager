using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using major_delivery_manager.Models;
using major_delivery_manager.Interfaces;
using Prism.Mvvm;
using System.Windows;
using Prism.Commands;

namespace major_delivery_manager.ViewModels
{
    internal class AddCourierViewModel : BindableBase
    {
        IRepository<CourierModel> repository;

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

        private DelegateCommand addCourierCommand;
        public DelegateCommand AddCourierCommand
        {
            get
            {
                return addCourierCommand ?? (new DelegateCommand(OnAddCourier, CanAddCourier));
            }
        }

        public AddCourierViewModel()
        {
            Courier = new CourierModel();
            repository = new CourierRepository();
        }

        private bool CanAddCourier() => true;
        private async void OnAddCourier()
        {
            if (!string.IsNullOrEmpty(Courier.FullName))
            {
                await repository.Create(Courier);
                MessageBox.Show("Курьер добавлен!");

                Courier = new CourierModel();
            }
            else
                MessageBox.Show("Не все поля заполнены");
        }
    }
}
