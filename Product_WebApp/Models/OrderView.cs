using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product_WebApp.Models
{
    public class OrderView
    {
        public int ID { get; set; }
        public string Customer_Name { get; set; }
        public string Fullname { get; set; }
        public string Address { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Date { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> CheckoutDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:0,0}", ApplyFormatInEditMode = true)]
        public Nullable<double> Total_Payment { get; set; }
        public string Status { get; set; }
    }
}