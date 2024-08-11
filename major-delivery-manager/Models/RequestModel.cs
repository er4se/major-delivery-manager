using major_delivery_manager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Prism.Mvvm;

namespace major_delivery_manager.Models
{
    internal class RequestModel : BindableBase
    {
        private IState<RequestState> _state;

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [NotMapped] public string status;

        [NotMapped] private string toCountry;
        [NotMapped] private string toSettlement;
        [NotMapped] private string fromCountry;
        [NotMapped] private string fromSettlement;
        [NotMapped] private string weight;
        [NotMapped] private string volume;
        [NotMapped] private string selfcost;
        [NotMapped] private string amount;

        public string Status
        {
            get => status;
            set
            {
                SetProperty(ref status, value);
            }
        }

        [Required]
        public string ToCountry
        {
            get => toCountry;
            set
            {
                SetProperty(ref toCountry, value);
            }
        }

        [Required]
        public string ToSettlement
        {
            get => toSettlement;
            set
            {
                SetProperty(ref toSettlement, value);
            }
        }

        [Required]
        public string FromCountry
        {
            get => fromCountry;
            set
            {
                SetProperty(ref fromCountry, value);
            }
        }

        [Required]
        public string FromSettlement
        {
            get => fromSettlement;
            set
            {
                SetProperty(ref fromSettlement, value);
            }
        }

        [Required]
        public string Weight
        {
            get => weight;
            set
            {
                SetProperty(ref weight, value);
            }
        }

        [Required]
        public string Volume
        {
            get => volume;
            set
            {
                SetProperty(ref volume, value);
            }
        }

        [Required]
        public string Selfcost
        {
            get => selfcost;
            set
            {
                SetProperty(ref selfcost, value);
            }
        }

        [Required]
        public string Amount
        {
            get => amount;
            set
            {
                SetProperty(ref amount, value);
            }
        }

        //[NotMapped] public CourierModel Responsible { get; set; }

        public RequestModel()
        {
            Id = Guid.NewGuid().ToString();
            _state = new RequestStateNew();

            Handle();
        }

        public void ChangeState(IState<RequestState> newState)
        {
            _state = newState;
        }

        public void Handle()
        {
            Status = _state.Handle();
        }

        public RequestState GetState()
        {
            return _state.GetState();
        }

        public void AssignCourier(CourierModel courier)
        {
            //Responsible = courier;
        }
    }
}
