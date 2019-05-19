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
    public class LoginController : Controller
    {
        private string url = "http://localhost:54105/api/Staff/";
        HttpClient httpClient = new HttpClient();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Staff staff)
        {
            var staffList = JsonConvert.DeserializeObjectAsync<IEnumerable<Staff>>(httpClient.GetStringAsync(url).Result).Result;
            var res = staffList.SingleOrDefault(s => s.Username == staff.Username && s.Password == staff.Password);
            if (res == null)
            {
                ModelState.AddModelError("", "username or password is invalid");
                return View();
            }
            else
            {
                Session["staff_id"] = res.ID.ToString();
                Session["store_id"] = res.Store_ID.ToString();
                Session["staff_name"] = res.Fullname.ToString();
                Session["order_date"] = DateTime.Now.ToShortDateString();

                if(res.Role == 1)
                {
                    return RedirectToAction("Manager", "Home");
                }
                else
                {
                    return RedirectToAction("Staff", "Home");
                }
            }
        }
    }
}