using major_delivery_manager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace major_delivery_manager
{
    internal class RequestStateDone : IState<RequestState>
    {
        public string Handle() => "ВЫПОЛНЕНА";
        public RequestState GetState()
        {
            return RequestState.DONE;
        }
    }
}
