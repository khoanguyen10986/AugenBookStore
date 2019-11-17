using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Common
{
    public class AircraftFactory : IDeliveryFactory
    {
        public IDelivery Create(double baseCost) => new Aircraft(baseCost);
    }
}
