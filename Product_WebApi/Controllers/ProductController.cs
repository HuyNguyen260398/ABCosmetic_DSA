using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Product_WebApi.Models;

namespace Product_WebApi.Controllers
{
    public class ProductController : ApiController
    {
        ABCosmeticEntities db = new ABCosmeticEntities();
        public IHttpActionResult GetProductList()
        {
            return Ok(db.Products.ToList());
        }
        public IHttpActionResult GetProduct(int id)
        {
            return Ok(db.Products.Find(id));
        }
    }
}
