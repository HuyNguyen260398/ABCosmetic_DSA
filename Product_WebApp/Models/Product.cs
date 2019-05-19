using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Product_WebApp.Models
{
    public class Product
    {
        public int ID { get; set; }
        public int Category_ID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public Nullable<double> Price { get; set; }
    }
}