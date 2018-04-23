using NewWebTrader.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewWebTrader.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        DownloadReport obj;
        public ReportController()
        {
            obj = new DownloadReport();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Upload()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Upload(Report model)
        {
            if (ModelState.IsValid)
            {
                bool success1 = false;
                var extension1 = Path.GetExtension(model.File1.FileName).ToLower();
                var path1 = Path.Combine(Server.MapPath("~/Content/Reports/"), model.Year + extension1);
                
                bool success2 = false;
                var extension2 = Path.GetExtension(model.File2.FileName).ToLower();
                var path2 = Path.Combine(Server.MapPath("~/Content/ReportThumb/"), model.Year + extension2);
                

                if (model.File1.ContentLength > 0)
                {
                    if (extension1 == ".pdf")
                    {
                        success1 = true;
                    }
                    else
                    {
                        ModelState.AddModelError("File1", "File extension should be .pdf");
                    }
                }
                if (model.File2.ContentLength > 0)
                {
                    if (extension2 == ".jpg")
                    {
                        success2 = true;
                    }
                    else
                    {
                        ModelState.AddModelError("File2", "File extension should be .jpg");
                    }
                }

                if(success1 && success2)
                {
                    model.File1.SaveAs(path1);
                    model.File2.SaveAs(path2);
                    return RedirectToAction("Index");
                }
            }
            

            return View(model);
        }

        [HttpGet]
        public ActionResult Index()
        {
            Uri url = System.Web.HttpContext.Current.Request.Url;
            string UrlLink = url.OriginalString.Replace(url.PathAndQuery, "");
            UrlLink = String.Concat(UrlLink, "/");

            var filecollection = obj.GetPdfFiles();
            var imgcollection = obj.GetImgFiles();
            List<ReportToView> objList = new List<ReportToView>();

            for(int i=0; i<filecollection.Count; i++)
            {
                objList.Add(new ReportToView()
                {
                    FileName = Path.GetFileNameWithoutExtension(filecollection[i].FileName),
                    FilePath = UrlLink + "Content/Reports/" + filecollection[i].FileName,
                    ImgPath = UrlLink + "Content/ReportThumb/" + imgcollection[i].FileName
                });
            }
            ViewBag.A = objList;
            return View();
        }


    }
}