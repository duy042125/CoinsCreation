using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.Database_Component
{
    public class Account
    {
        public Account(string theUsername, string thePassword, string theName, DateTime? theBirthdate, string thePhoneNumber, string theSSN)
        {
            username = theUsername;
            password = thePassword;
            full_name = theName;
            SSN = theSSN;
            birthdate = theBirthdate;
            phone_number = thePhoneNumber;
        }

        public Account()
        {
            username = null;
            password = null;
            full_name = null;
            birthdate = null;
            phone_number = null;
            SSN = null;
        }

        public string username { get; set; }
        public string password { get; set; }
        public string full_name { get; set; }
        public DateTime? birthdate { get; set; }
        public string phone_number { get; set; }
        public string SSN { get; set; }
    }
}
