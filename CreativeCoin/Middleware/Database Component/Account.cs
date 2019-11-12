using System;

namespace Middleware.Database_Component
{
    public class Account
    {
        public string username { get; set; }
        public string password { get; set; }
        public string full_name { get; set; }
        public DateTime? birthdate { get; set; }
        public string phone_number { get; set; }
        public char type { get; set; }
    }
}
