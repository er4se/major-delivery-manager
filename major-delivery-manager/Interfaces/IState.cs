using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace major_delivery_manager
{
    interface IState<T> where T : Enum
    {
        T GetState();
        string Handle();
    }
}
