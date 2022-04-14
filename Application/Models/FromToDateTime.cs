using Application.Extensions;

namespace Application.Models
{
    public class FromToDateTime
    {

        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }

        public FromToDateTime()
        {
            StartDate = DateTime.Now.AddDays(1);
            EndDate = DateTime.Now.AddDays(1).AddHours(1);
        }
        public FromToDateTime(string startDate, string endDate)
        {
            StartDate = TryParseToDateTime(startDate);
            EndDate = TryParseToDateTime(endDate);
            if(StartDate>EndDate)
                throw new ArgumentException("End date can't before start date.");
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