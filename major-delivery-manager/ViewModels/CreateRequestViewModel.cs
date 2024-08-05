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

            db.Database.EnsureCreated();
            db.Requests.Load();
            CreateRequests = db.Requests.Local.ToObservableCollection();

            deliveryRequestModel.Id = Guid.NewGuid().ToString();

            createCommand = new DelegateCommand(() =>
            {
                db.Requests.Add(deliveryRequestModel);
                db.SaveChanges();

                //deliveryRequestModel.Id = Guid.NewGuid().ToString();

                CreateRequests = db.Requests.Local.ToObservableCollection();

                foreach(var elem in CreateRequests)
                {
                    MessageBox.Show(elem.Discription);
                }
            });
        }

        private DeliveryRequestModel deliveryRequestModel = new DeliveryRequestModel();
        public string DeliveryRequestModelDisc
        {
            get { return deliveryRequestModel.Discription; }
            set 
            {
                deliveryRequestModel.Discription = value;
                RaisePropertyChanged(nameof(DeliveryRequestModel));
            }
        }
        public ObservableCollection<DeliveryRequestModel> CreateRequests { get; set; }

        ApplicationContext db = new ApplicationContext();

        public DelegateCommand createCommand { get; }
    }
}
