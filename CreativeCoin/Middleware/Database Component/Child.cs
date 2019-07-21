using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.Database_Component
{
    public class Child
    {
        public Child(string theParent_username, string theChild_name, DateTime? theBirthdate, int theTotal_coin = 0)
        {
            Parent_username = theParent_username;
            Child_name = theChild_name;
            birthdate = theBirthdate;
            total_coin = theTotal_coin;
        }

        public Child()
        {
            Parent_username = null;
            Child_name = null;
            birthdate = null;
            total_coin = 0;
        }

        public string Parent_username;
        public string Child_name;
        public DateTime? birthdate;
        public int total_coin;

        public void addTotal_coin(int newCoin)
        {
            total_coin += newCoin;
        }
    }
}
