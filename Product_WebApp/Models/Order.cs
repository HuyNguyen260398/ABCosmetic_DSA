using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product_WebApp.Models
{
    public class Order
    {
        public int ID { get; set; }
        public int Staff_ID { get; set; }
        [Required(ErrorMessage = "customer name is required")]
        [StringLength(50)]
        public string Customer_Name { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<double> Total_Payment { get; set; }
        public Nullable<int> Store_ID { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> CheckoutDate { get; set; }
    }
}