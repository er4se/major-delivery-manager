using major_delivery_manager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace major_delivery_manager
{
    class RequestStateInProccess : IState<RequestState>
    {
        public void Handle() { }
        public RequestState GetState()
        {
            return RequestState.INPROCCESS;
        }
    }
}

