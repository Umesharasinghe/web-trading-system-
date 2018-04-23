using NewWebTrader.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace NewWebTrader.Controllers
{
    [Authorize]
    public class LearnController : Controller
    {
        DownloadFile obj;
        public LearnController()
        {
            obj = new DownloadFile();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult UploadFiles()
        {
            return View();
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult UploadFiles(HttpPostedFileBase file)
        {
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/uploads/"), file.FileName);
                    if (Path.GetExtension(path).ToLower() == ".pdf")
                    {
                        file.SaveAs(path);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("file", "File extension is not supported!");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("file", "Please select a file");
            }
            
            return View(file);
        }


        
        [HttpGet]
        public ActionResult Index()
        {
            var filesCollection = obj.GetFiles();
            
            ViewBag.A = filesCollection;
            return View();
        }

        [HttpPost]
        public FileResult Download(string FileId)
        {
            int CurrentFileID = Convert.ToInt32(FileId);
            var filesCol = obj.GetFiles();
            string CurrentFileName = (from fls in filesCol
                                      where fls.FileId == CurrentFileID
                                      select fls.FilePath).First();

            string contentType = string.Empty;

            if (CurrentFileName.Contains(".pdf"))
            {
                contentType = "application/pdf";
            }

            else if (CurrentFileName.Contains(".docx"))
            {
                contentType = "application/docx";
            }
            return File(CurrentFileName, contentType, CurrentFileName);
        }
    }
}


























































































































































































































































































