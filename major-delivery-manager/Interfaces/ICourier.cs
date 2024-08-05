using major_delivery_manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace major_delivery_manager.Interfaces
{
    interface ICourier
    {
        string Id { get; }
        string Name { get; }
        CourierState State { get; }
        IDeliveryRequest ActiveDelivery { get; }

        public void EntryRequest() { }
        public void QuitRequest() { }
        public void UpdateState() { }
    }
}
