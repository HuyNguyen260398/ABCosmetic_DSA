using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;
using Product_WebApp.Models;

namespace Product_WebApp.Models
{
    public class Cart
    {
        private string url = "http://localhost:63380/api/Product/";
        HttpClient httpClient = new HttpClient();
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public double ProductPrice { get; set; }
        public int Quantity { get; set; }
        public double Subtotal
        {
            get { return Quantity * ProductPrice; }
        }
        public Cart(int ProID)
        {
            var model = JsonConvert.DeserializeObjectAsync<Product>(httpClient.GetStringAsync(url + ProID).Result).Result;
            ProductId = ProID;
            ProductName = model.Name;
            ProductImage = model.Image;
            ProductPrice = double.Parse(model.Price.ToString());
            Quantity = 1;
        }
    }
}