using major_delivery_manager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace major_delivery_manager
{
    internal class RequestStateNew : IState<RequestState>
    {
        public string Handle() => "НОВАЯ";
        public RequestState GetState()
        {
            return RequestState.NEW;
        }
    }
}
