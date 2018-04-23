using NewWebTrader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewWebTrader.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        ApplicationDbContext _context;

        public EventController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var eventList = _context.EventsUploads.OrderByDescending(c => c.UploadedDate);
            ViewBag.First = eventList.FirstOrDefault();
            ViewBag.EventList = eventList;
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Upload(EventsUpload model)
        {
            if (ModelState.IsValid)
            {
                model.UploadedDate = DateTime.Now;
                _context.EventsUploads.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            EventsUpload news = _context.EventsUploads.Find(id);
            _context.EventsUploads.Remove(news);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}