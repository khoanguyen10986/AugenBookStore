using System;
using System.Globalization;

namespace BookStore.Common
{
    public class MotoBike : IDelivery
    {
        private readonly double _cost;

        public double DeliveryCost
        {
            get
            {
                double cost = _cost;

                int currentMonth = DateTime.Now.Month;
                if (currentMonth >= 6 && currentMonth <= 8)
                {
                    cost = _cost * 0.5;
                }
                else if (currentMonth == 9)
                {
                    cost = _cost * 1.5;
                }

                return cost;
            }
        }

        public MotoBike(double cost)
        {
            _cost = cost;
        }

        public string GetInfo()
        {
            var random = new Random();

            var driverName = random.GetRandomText(10, "DriverName");
            var mobile = random.GetRandomPhoneNumber().ToString();
            var deliveryDate = random.GetRandomDate(DateTime.Now.AddDays(1), DateTime.Now.AddDays(5)).ToString("dddd, dd MMMM yyyy", new CultureInfo("en-US"));
            var cost = DeliveryCost.ToString();

            return $"Delivery info: Driver name: {driverName}. Mobile: {mobile}. Delivery date: {deliveryDate}. Cost: {cost}";
        }
    }
}