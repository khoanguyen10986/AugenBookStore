using System;
using System.Text;

namespace BookStore.Common
{
    public static class RandomExtension
    {
        public static string GetRandomText(this Random random, int size, string prefix = "")
        {
            char character;
            var builder = new StringBuilder();

            for (int i = 0; i < size; i++)
            {
                character = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(character);
            }

            return $"{prefix} {builder.ToString()}".TrimStart();
        }

        public static string GetRandomPhoneNumber(this Random random)
        {
            var telNo = new StringBuilder(12);
            int number;
            
            for (int i = 0; i < 3; i++)
            {
                number = random.Next(0, 8);
                telNo = telNo.Append(number.ToString());
            }
            telNo = telNo.Append("-");
            number = random.Next(0, 743);
            telNo = telNo.Append(String.Format("{0:D3}", number));
            telNo = telNo.Append("-");
            number = random.Next(0, 10000);
            telNo = telNo.Append(String.Format("{0:D4}", number));
            return telNo.ToString();
        }

        public static DateTime GetRandomDate(this Random random, DateTime dateFrom, DateTime dateTo)
        {
            TimeSpan range = dateTo - dateFrom;
            int days = range.Days * 60 * 60 * 24;
            int hours = range.Hours * 60 * 60;
            int minutes = range.Minutes * 60;
            int seconds = range.Seconds;
            int randomRange = days + hours + minutes + seconds;
            int numberOfSecondsToAdd = random.Next(randomRange);

            DateTime result = dateFrom.AddSeconds(numberOfSecondsToAdd);
            return result;
        }
    }
}