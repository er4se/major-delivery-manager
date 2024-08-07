using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using major_delivery_manager.Interfaces;
using major_delivery_manager.Models;

namespace major_delivery_manager
{
    class DeliverRequestCommand : IRequestCommand
    {
        public void Execute(RequestModel request)
        {
            request.ChangeState(new RequestStateDone());
        }
    }
}
