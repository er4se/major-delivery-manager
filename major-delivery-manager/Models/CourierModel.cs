using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using major_delivery_manager.Interfaces;

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

        public CourierModel()
        {
            Id = Guid.NewGuid().ToString();
            acceptCommand = new AcceptRequestCommand();
            cancelCommand = new CancelRequestCommand();
            deliverCommand = new DeliverRequestCommand();
        }

        public void AcceptRequest(RequestModel request)
        {
            acceptCommand.Execute(request);
        }

        public void CancelRequest(RequestModel request) 
        { 
            cancelCommand.Execute(request); 
        }

        public void DeliverRequest(RequestModel request)
        {
            deliverCommand.Execute(request);
        }
    }
}
