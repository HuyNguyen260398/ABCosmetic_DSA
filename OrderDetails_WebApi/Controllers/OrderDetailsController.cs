using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OrderDetails_WebApi.Models;

namespace OrderDetails_WebApi.Controllers
{
    public class OrderDetailsController : ApiController
    {
        ABCosmeticEntities db = new ABCosmeticEntities();
        public IHttpActionResult GetOrderDetailsList()
        {
            List<Order_Detail> orderList = db.Order_Details.ToList();
            return Ok(orderList);
        }
        public IHttpActionResult PostOrderDetails(Order_Detail model)
        {
            db.Order_Details.Add(model);
            db.SaveChanges();
            return Ok();
        }
        public IHttpActionResult GetOrderDetails(int id)
        {
            return Ok(db.Order_Details.Find(id));
        }
    }
}
