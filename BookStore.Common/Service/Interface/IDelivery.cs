using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Common
{
    public interface IDelivery
    {
        double DeliveryCost { get; }
        string GetInfo();
    }
}
