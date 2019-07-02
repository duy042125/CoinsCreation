using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.Database_Component
{
    public class Account
    {
        #region Table rows
        public Account(string theUsername, string thePassword, string theName)
        {
            username = theUsername;
            password = Hashing.HashPassword(thePassword);

        }
        public string username { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        #endregion
    }
}
