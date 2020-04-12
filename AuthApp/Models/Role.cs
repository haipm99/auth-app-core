using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApp.Models
{
    public class Role
    {
        public string id { get; set; }
        public string name { get; set; }
        public string desc { get; set; }

        public List<User> users { get; set; }
    }
}
