using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using Product_WebApp.Models;

namespace Product_WebApp.Controllers
{
    public class ProductController : Controller
    {
        private string url = "http://localhost:63380/api/Product/";
        HttpClient httpClient = new HttpClient();
        // GET: Product
        public ActionResult Index()
        {
            var model = JsonConvert.DeserializeObjectAsync<IEnumerable<Product>>(httpClient.GetStringAsync(url).Result).Result;
            return View(model);
        }
    }
}