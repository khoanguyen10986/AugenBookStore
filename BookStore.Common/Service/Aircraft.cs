using System;
using System.Globalization;

namespace BookStore.Common
{
    public class Aircraft : IDelivery
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
                    cost = _cost * 0.8;
                }
                else if (currentMonth == 9)
                {
                    cost = _cost * 2;
                }

                return cost;
            }
        }

        public Aircraft(double cost)
        {
            _cost = cost;
        }

        public string GetInfo()
        {
            var random = new Random();

            var flightNo = random.Next(100, 900).ToString();
            var gateOfArrival = random.GetRandomText(10, "Gate");
            var dateOfArrival = random.GetRandomDate(DateTime.Now.AddDays(1), DateTime.Now.AddDays(5)).ToString("dddd, dd MMMM yyyy", new CultureInfo("en-US"));
            var cost = DeliveryCost.ToString();

            return $"Delivery info: Flight no: {flightNo}. Gate of arrival: {gateOfArrival}. Date of arrival: {dateOfArrival}. Cost: {cost}";
        }
    }
}
