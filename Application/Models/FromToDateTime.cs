using System.Text.Json.Serialization;

namespace Application.Models
{
    public class FromToDateTime
    {

        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        public FromToDateTime(string startDate, string endDate)
        {
            StartDate = TryParseToDateTime(startDate);
            EndDate = TryParseToDateTime(endDate);
            if (StartDate > EndDate)
                throw new ArgumentException("End date can't before start date.");
        }

        [JsonConstructorAttribute]
        public FromToDateTime(DateTime startDate, DateTime endDate)
        {
            if (StartDate > EndDate)
                throw new ArgumentException("End date can't before start date.");
            StartDate = startDate;
            EndDate = endDate;
        }

        private static DateTime TryParseToDateTime(string dateTimeStr)
        {
            DateTime dateTime;
            try
            {
                dateTime = DateTime.Parse(dateTimeStr);
            }
            catch (FormatException)
            {
                throw new FormatException("Given string didn.t matched date format.");
            }
            return dateTime;
        }

    }
}