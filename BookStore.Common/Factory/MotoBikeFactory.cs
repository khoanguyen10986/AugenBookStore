using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Common
{
    public class MotoBikeFactory : IDeliveryFactory
    {
        public IDelivery Create(double baseCost) => new MotoBike(baseCost);
    }
}