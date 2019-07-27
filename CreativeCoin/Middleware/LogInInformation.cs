using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware
{
    public class LogInInformation
    {
        public static string Username { get; set; }
        public static string Child_name { get; set; }
        public static string Behavior_name { get; set; }
        public static int coin_earned { get; set; }

        public static void Clear()
        {
            Username = null;
            Child_name = null;
            Behavior_name = null;
            coin_earned = 0;
        }
    }
}
