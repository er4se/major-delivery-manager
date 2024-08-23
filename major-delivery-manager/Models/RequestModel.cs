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
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace major_delivery_manager.Models
{
    internal class RequestModel : BindableBase
    {
        private IState<RequestState> _state;
        private static readonly Regex onlyNumbers = new Regex("[^0-9.-]+");

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [ForeignKey("CourierModel")]
        public string? ResponsibleId { get; set; }

        [NotMapped] public string status;

        [NotMapped] private string toCountry;
        [NotMapped] private string toSettlement;
        [NotMapped] private string fromCountry;
        [NotMapped] private string fromSettlement;
        [NotMapped] private string weight;
        [NotMapped] private string volume;
        [NotMapped] private string selfcost;
        [NotMapped] private string amount;

        public virtual RequestCancellationModel CancellationModel { get; set; }
        public virtual CourierModel CourierModel { get; set; }

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
                if (this.GetState() == RequestState.NEW)
                    SetProperty(ref toCountry, value);
            }
        }

        [Required]
        public string ToSettlement
        {
            get => toSettlement;
            set
            {
                if (this.GetState() == RequestState.NEW)
                    SetProperty(ref toSettlement, value);
            }
        }

        [Required]
        public string FromCountry
        {
            get => fromCountry;
            set
            {
                if (this.GetState() == RequestState.NEW)
                    SetProperty(ref fromCountry, value);
            }
        }

        [Required]
        public string FromSettlement
        {
            get => fromSettlement;
            set
            {
                if (this.GetState() == RequestState.NEW)
                    SetProperty(ref fromSettlement, value);
            }
        }

        [Required]
        public string Weight
        {
            get => weight;
            set
            {
                if ((this.GetState() == RequestState.NEW) && !onlyNumbers.IsMatch(value))
                    SetProperty(ref weight, value);
            }
        }

        [Required]
        public string Volume
        {
            get => volume;
            set
            {
                if ((this.GetState() == RequestState.NEW) && !onlyNumbers.IsMatch(value))
                    SetProperty(ref volume, value);
            }
        }

        [Required]
        public string Selfcost
        {
            get => selfcost;
            set
            {
                if ((this.GetState() == RequestState.NEW) && !onlyNumbers.IsMatch(value))
                    SetProperty(ref selfcost, value);
            }
        }

        [Required]
        public string Amount
        {
            get => amount;
            set
            {
                if ((this.GetState() == RequestState.NEW) && !onlyNumbers.IsMatch(value))
                    SetProperty(ref amount, value);
            }
        }

        public RequestModel()
        {
            Id = Guid.NewGuid().ToString();
            ResponsibleId = null;

            _state = new RequestStateNew();

            Handle();
        }

        public void ChangeState(IState<RequestState> newState)
        {
            _state = newState;
            Handle();
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
            ResponsibleId = courier.Id;
        }

        public void EnsureState() 
        {
            switch (Status)
            {
                case "ВЫПОЛНЯЕТСЯ":
                    _state = new RequestStateInProccess();
                    break;
                case "ВЫПОЛНЕНА":
                    _state = new RequestStateDone();
                    break;
                case "ОТМЕНЕНА":
                    _state = new RequestStateCanceled();
                    break;
                default:
                    _state = new RequestStateNew();
                    break;
            }
        }
    }
}
