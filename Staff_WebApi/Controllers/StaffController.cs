using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Staff_WebApi.Models;

namespace Staff_WebApi.Controllers
{
    public class StaffController : ApiController
    {
        ABCosmeticEntities db = new ABCosmeticEntities();
        public IHttpActionResult GetStaffList()
        {
            List<User> staffList = db.Users.ToList();
            return Ok(staffList);
        }
        public IHttpActionResult GetStaffList(int? role)
        {
            var res = db.Users.AsQueryable();
            if (role == null)
            {
                return Ok(res.ToList());
            }
            else
            {
                res = res.Where(s => s.Role == role);
                return Ok(res);
            }
        }
        public IHttpActionResult PostStaff(User model)
        {
            db.Users.Add(model);
            db.SaveChanges();
            return Ok();
        }
        public IHttpActionResult GetStaff(int id)
        {
            return Ok(db.Users.Find(id));
        }
    }
}
