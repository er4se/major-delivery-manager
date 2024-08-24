using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using major_delivery_manager.Models;

namespace major_delivery_manager.Miscellaneous
{
    internal static class RequestStateConverter
    {
        static public void FromString(RequestModel model)
        {
            switch (model.Status)
            {
                case "ВЫПОЛНЯЕТСЯ":
                    model.ChangeState(new RequestStateInProccess());
                    break;
                case "ВЫПОЛНЕНА":
                    model.ChangeState(new RequestStateDone());
                    break;
                case "ОТМЕНЕНА":
                    model.ChangeState(new RequestStateCanceled());
                    break;
                default:
                    model.ChangeState(new RequestStateNew());
                    break;
            }
        }
    }
}
