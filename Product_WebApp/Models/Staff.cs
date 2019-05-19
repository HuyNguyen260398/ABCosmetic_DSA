using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Product_WebApp.Models
{
    public class Staff
    {
        public int ID { get; set; }
        public int Store_ID { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public Nullable<int> Role { get; set; }
    }
}