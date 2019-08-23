using System;

namespace Middleware.Database_Component
{
    public class Child
    {
        public string Parent_username { get; set; }
        public string Child_name { get; set; }
        public DateTime? birthdate { get; set; }
        public DateTime? start_date { get; set; }
        public int total_coin { get; set; }
    }
}
