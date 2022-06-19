using System.Collections.Generic;

namespace ApiDairy.Models
{
    public class Role
    {
        public int roleid { get; set; }
        public string name { get; set; }
        public List<User> users { get; set; }
        public Role()
        {
            users = new List<User>();
        }
    }
}
