using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Order_WebApi.Models;

namespace Order_WebApi.Controllers
{
    public class OrderController : ApiController
    {
        ABCosmeticEntities1 db = new ABCosmeticEntities1();
        public IHttpActionResult GetOrderList()
        {
            List<Order> orderList = db.Orders.ToList();
            return Ok(orderList);
        }
        public IHttpActionResult PostOrder(Order model)
        {
            db.Orders.Add(model);
            db.SaveChanges();
            return Ok();
        }
        public IHttpActionResult GetOrder(string cname)
        {
            var order = db.Orders.Where(o => o.Customer_Name.Equals(cname));
            return Ok(order);
        }
        public IHttpActionResult PutOrder(Order model)
        {
            var res = db.Orders.SingleOrDefault(m => m.ID == model.ID);
            if (res != null)
            {
                res.Staff_ID = model.Staff_ID;
                res.Store_ID = model.Store_ID;
                res.Customer_Name = model.Customer_Name;
                res.Date = model.Date;
                res.CheckoutDate = model.CheckoutDate;
                res.Total_Payment = model.Total_Payment;
                res.Status = model.Status;
                db.SaveChanges();
            }
            return Ok();
        }
        public IHttpActionResult DeleteOrder(int id)
        {
            var model = db.Orders.SingleOrDefault(e => e.ID.Equals(id));
            if (model != null)
            {
                db.Orders.Remove(model);
                db.SaveChanges();
            }
            return Ok();
        }
    }
}
