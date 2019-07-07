using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware
{
    public class DateTimeConverter
    {
        public static DateTime toDateTime(string stringDate)
        {
            string[] date = stringDate.Split('/');
            try
            {
                DateTime dateTime = new DateTime(int.Parse(date[2]), int.Parse(date[0]), int.Parse(date[1]));
                return dateTime;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }
}
