using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using major_delivery_manager.Interfaces;
using System.Windows;

namespace major_delivery_manager.Models
{
    internal class CourierModel : BindableBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [NotMapped] private string fullName;

        [Required]
        public string FullName
        {
            get => fullName;
            set
            {
                SetProperty(ref fullName, value);
            }
        }

        [NotMapped] private readonly IRequestCommand acceptCommand;
        [NotMapped] private readonly IRequestCommand cancelCommand;
        [NotMapped] private readonly IRequestCommand deliverCommand;

        public virtual ICollection<RequestModel> RequestModels { get; set; }

        public CourierModel()
        {
            Id = Guid.NewGuid().ToString();
            acceptCommand = new AcceptRequestCommand();
            cancelCommand = new CancelRequestCommand();
            deliverCommand = new DeliverRequestCommand();
        }

        public CourierModel(CourierModel model)
        {
            this.Id = model.Id;
            this.FullName = model.FullName;

            acceptCommand = new AcceptRequestCommand();
            cancelCommand = new CancelRequestCommand();
            deliverCommand = new DeliverRequestCommand();
        }

        public void ExecuteCommand(RequestModel request)
        {
            switch (request.GetState())
            {
                case RequestState.NEW:
                    acceptCommand.Execute(request);
                    break;
                case RequestState.INPROCCESS:
                    deliverCommand.Execute(request);
                    break;
                case RequestState.DONE:
                    MessageBox.Show("Доставка выполнена!");
                    break;
                case RequestState.CANCELED:
                    MessageBox.Show("Заявка отменена!");
                    break;
            }
        }

        public void CancelCommand(RequestModel request)
        {
            cancelCommand.Execute(request);
        }
    }
}
