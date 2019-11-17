using System;
using System.Globalization;

namespace BookStore.Common
{
    public class Train : IDelivery
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
                    cost = _cost * 1.8;
                }

                return cost;
            }
        }

        public Train(double cost)
        {
            _cost = cost;
        }

        public string GetInfo()
        {
            var random = new Random();

            var trainNo = random.Next(100, 900).ToString();
            var stationOfArrival = random.GetRandomText(10, "Station");
            var dateOfArrival = random.GetRandomDate(DateTime.Now.AddDays(1), DateTime.Now.AddDays(5)).ToString("dddd, dd MMMM yyyy", new CultureInfo("en-US"));
            var cost = DeliveryCost.ToString();

            return $"Delivery info: Train no: {trainNo}. Station of arrival: {stationOfArrival}. Date of arrival: {dateOfArrival}. Cost: {cost}";
        }
    }
}
