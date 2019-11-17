using System;
using System.Collections.Generic;

namespace BookStore.Common
{
    public interface IDeliveryServiceGenerator
    {
        IDelivery ExecuteCreation(DeliveryType serviceType, double cost);
    }

    public class DeliveryServiceGenerator : IDeliveryServiceGenerator
    {
        private readonly Dictionary<DeliveryType, IDeliveryFactory> _factories;

        public DeliveryServiceGenerator()
        {
            _factories = new Dictionary<DeliveryType, IDeliveryFactory>();

            foreach (DeliveryType serviceType in Enum.GetValues(typeof(DeliveryType)))
            {
                string className = Enum.GetName(typeof(DeliveryType), serviceType);
                var factory = Activator.CreateInstance(Type.GetType($"BookStore.Common.{className}Factory")) as IDeliveryFactory;
                _factories.Add(serviceType, factory);
            }
        }

        public IDelivery ExecuteCreation(DeliveryType serviceType, double cost) => _factories[serviceType].Create(cost);
    }
}