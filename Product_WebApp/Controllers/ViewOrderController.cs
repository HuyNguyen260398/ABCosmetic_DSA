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
    public class ViewOrderController : Controller
    {
        private string url = "http://localhost:58433/api/OrderView/";
        private string url_staff = "http://localhost:54105/api/Staff/";
        private string url_orderdetails = "http://localhost:52517/api/OrderDetails/";
        HttpClient httpClient = new HttpClient();
        // GET: ViewOrder
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult OrderDetails(int? id)
        {
            var order = JsonConvert.DeserializeObjectAsync<OrderView>(httpClient.GetStringAsync(url + id).Result).Result;
            var orderDetails = JsonConvert.DeserializeObjectAsync<IEnumerable<OrderDetails>>(httpClient.GetStringAsync(url_orderdetails).Result).Result.AsQueryable();
            orderDetails = orderDetails.Where(o => o.Order_ID == id);
            Session["Order-Customer"] = order.Customer_Name;
            Session["Order-Staff"] = order.Fullname;
            Session["Order-Store"] = order.Address;
            Session["Order-Date"] = String.Format("{0:dd-MM-yyyy}", order.Date);
            Session["Purchase-Date"] = String.Format("{0:dd-MM-yyyy}", order.CheckoutDate);
            Session["Order-Payment"] = order.Total_Payment;
            Session["Order-Status"] = order.Status;
            return View(orderDetails);
        }
        public ActionResult StaffList(string name)
        {
            var staffList = JsonConvert.DeserializeObjectAsync<IEnumerable<Staff>>(httpClient.GetStringAsync(url_staff).Result).Result.AsQueryable();
            if(name == null)
            {
                staffList = staffList.Where(s => s.Role == 2);
            }
            else if(name != null)
            {
                staffList = staffList.Where(s => s.Fullname.Contains(name) || name == null);
            }
            return View(staffList.ToList());
        }
        public ActionResult StaffOrder(string name, DateTime? from, DateTime? to, DateTime? date)
        {
            var orderList = JsonConvert.DeserializeObjectAsync<IEnumerable<OrderView>>(httpClient.GetStringAsync(url).Result).Result.AsQueryable();
            if (from == null && to == null && date == null)
            {
                orderList = orderList.Where(s => s.Fullname.Equals(name));
                Session["staff"] = name;

                var totalOrder = orderList.Count();
                var totalRevenue = orderList.Sum(o => o.Total_Payment);

                ViewBag.TotalOrder = totalOrder;
                ViewBag.TotalRevenue = totalRevenue;
            }
            else if (from != null && to != null && date == null)
            {
                orderList = orderList.Where(x => x.Date >= from && x.Date <= to || from == null || to == null);

                var totalOrder = orderList.Count();
                var totalRevenue = orderList.Sum(o => o.Total_Payment);

                ViewBag.TotalOrder = totalOrder;
                ViewBag.TotalRevenue = totalRevenue;
            }
            else if(from == null && to == null && date != null)
            {
                orderList = orderList.Where(x => x.Date == date || date == null);

                var totalOrder = orderList.Count();
                var totalRevenue = orderList.Sum(o => o.Total_Payment);

                ViewBag.TotalOrder = totalOrder;
                ViewBag.TotalRevenue = totalRevenue;
            }
            return View(orderList.ToList());
        }
    }
}