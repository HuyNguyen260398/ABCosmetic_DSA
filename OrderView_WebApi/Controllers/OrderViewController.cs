using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OrderView_WebApi.Models;

namespace OrderView_WebApi.Controllers
{
    public class OrderViewController : ApiController
    {
        ABCosmeticEntities db = new ABCosmeticEntities();
        public IHttpActionResult GetOrderList()
        {
            var model = db.ViewOrders.ToList();
            return Ok(model);
        }
        public IHttpActionResult GetStaffOrder(string name)
        {
            var res = db.ViewOrders.Where(s => s.Fullname.Equals(name));
            return Ok(res);
        }
        public IHttpActionResult GetOrder(int id)
        {
            return Ok(db.ViewOrders.Find(id));
        }
    }
}
