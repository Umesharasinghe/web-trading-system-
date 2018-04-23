using NewWebTrader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NewWebTrader.Controllers
{
    public class EventDetailsController : ApiController
    {
        ApplicationDbContext _context;

        public EventDetailsController()
        {
            _context = new ApplicationDbContext();
        }

        [Route("api/event/eventdetails")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            return Json(_context.EventsUploads.Select(c => new { Title = c.Title, Content = c.Content }));
        }

        //[Route("api/event/eventdetails/{id}")]
        //[HttpGet]
        //public IHttpActionResult GetById(int id)
        //{
        //    return Json(_context.EventsUploads.Where(c => c.Id == id));
        //}
    }
}
