using major_delivery_manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace major_delivery_manager.Interfaces
{
    interface IDeliveryRequest
    {
        string Id { get; }
        string CargoDiscription { get; }
        ICustomer Customer { get; set; }
        ICourier Courier { get; set; }
        DeliveryRequestState State { get; set; }

        public void UpdateState() { }
    }
}
