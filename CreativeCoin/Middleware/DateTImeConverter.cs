using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware
{
    public class DateTimeConverter
    {
        public static DateTime? toDateTime(string stringDate)
        {
            if (stringDate.Equals("")) return null;
            try
            {
                string[] date = stringDate.Split('/');
                DateTime? dateTime = new DateTime(int.Parse(date[2]), int.Parse(date[0]), int.Parse(date[1]));
                return dateTime;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public static string dateTimeToString(DateTime? date)
        {
            string dateString = date.ToString();
            string[] dateArr = dateString.Split(' ');
            return dateArr[0];
        }

        public static string timeSpanToAge(TimeSpan? theAge)
        {
            if(theAge.HasValue)
            {
                TimeSpan childAge = theAge.Value;
                double daysInYear = 365.25;
                int age = (int)(childAge.TotalDays / daysInYear);
                return age.ToString();
            }
            return "Null";
        }

        public static string timeSpanToWeek(TimeSpan? theWeek)
        {
            if (theWeek.HasValue)
            {
                TimeSpan progressWeek = theWeek.Value;
                double daysInWeek = 7;
                int week = (int)(progressWeek.TotalDays / daysInWeek) + 1;
                return week.ToString();
            }
            return "Null";
        }
    }
}
