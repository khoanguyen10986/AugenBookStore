using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Common
{
    public interface IDeliveryFactory
    {
        IDelivery Create(double baseCost);
    }
}
