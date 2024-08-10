using major_delivery_manager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace major_delivery_manager.Models
{
    internal class RequestModel
    {
        private IState<RequestState> _state;

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string ToCountry { get; set; } = string.Empty;
        [Required]
        public string FromCountry { get; set; } = string.Empty;
        [Required]
        public string ToSattlement { get; set; } = string.Empty;
        [Required]
        public string FromSattlement { get; set; } = string.Empty;
        [Required]
        public int Weight { get; set; }
        [Required]
        public int Volume { get; set; }
        [Required]
        public int Cost { get; set; }
        [Required]
        public int Amount { get; set; }
        [NotMapped]
        public CourierModel Responsible { get; set; }

        public RequestModel()
        {
            Id = Guid.NewGuid().ToString();
            _state = new RequestStateNew();
        }

        public void ChangeState(IState<RequestState> newState)
        {
            _state = newState;
        }

        public void Handle()
        {
            _state.Handle();
        }

        public RequestState GetState()
        {
            return _state.GetState();
        }

        public void AssignCourier(CourierModel courier)
        {
            Responsible = courier;
        }
    }
}
