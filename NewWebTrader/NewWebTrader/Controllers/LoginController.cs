using NewWebTrader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NewWebTrader.Controllers
{
    public class LoginController : ApiController
    {
        ApplicationDbContext _context;

        public LoginController()
        {
            _context = new ApplicationDbContext();
        }

        //public IHttpActionResult GetAll()
        //{
        //    return Json(_context.Users.Select(c => new { c.UserName, c.Password }));
        //}

        [Route("api/login/logindetails/{email}/{password}")]
        [HttpGet]
        public bool Getby(string email, string password)
        {
            var pass = _context.Users.Where(c => c.UserName == email).FirstOrDefault();
            if(pass.Password == password)
            {
                return true;
            }
            return false;
        }
    }
}
