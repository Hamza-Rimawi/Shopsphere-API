using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Models.Enums
{
    public enum ShippingStatus
    {
        Processing = 1,
        OutForDelivery = 2,
        Delivered = 3,
        ReturnToSender = 4,
        OnHold = 5,
        Delayed = 6,
        Lost = 7
    }

}
