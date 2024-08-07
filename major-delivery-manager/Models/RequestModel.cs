using major_delivery_manager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace major_delivery_manager.Models
{
    class RequestModel
    {
        private IState<RequestState> _state;

        public string Id { get; }
        public string ToCountry { get; set; } = string.Empty;
        public string FromCountry { get; set; } = string.Empty;
        public string ToSattlement { get; set; } = string.Empty;
        public string FromSattlement { get; set; } = string.Empty;
        public int Weight { get; set; }
        public int Volume { get; set; }
        public int Cost { get; set; }
        public int Amount { get; set; }
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
