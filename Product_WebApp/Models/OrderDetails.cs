using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Product_WebApp.Models
{
    public class OrderDetails
    {
        public int ID { get; set; }
        public int Order_ID { get; set; }
        public int Product_ID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<double> Subtotal { get; set; }
    }
}