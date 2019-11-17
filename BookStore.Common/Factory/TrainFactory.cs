using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Common
{
    public class TrainFactory : IDeliveryFactory
    {
        public IDelivery Create(double baseCost) => new Train(baseCost);
    }
}
