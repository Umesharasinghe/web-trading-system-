using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace NewWebTrader.Models
{
    public class DownloadReport
    {
        public List<DownloadReportInfo> GetPdfFiles()
        {
            List<DownloadReportInfo> lstFiles = new List<DownloadReportInfo>();
            DirectoryInfo dirInfo = new DirectoryInfo(HostingEnvironment.MapPath("~/Content/Reports"));
            
            foreach (var item in dirInfo.GetFiles())
            {
                lstFiles.Add(new DownloadReportInfo()
                {
                    FileName = item.Name,
                    FilePath = dirInfo.FullName + @"\" + item.Name
                });
            }
            return lstFiles;
        }

        public List<DownloadReportInfo> GetImgFiles()
        {
            List<DownloadReportInfo> lstFiles = new List<DownloadReportInfo>();
            DirectoryInfo dirInfo = new DirectoryInfo(HostingEnvironment.MapPath("~/Content/ReportThumb"));
            
            foreach (var item in dirInfo.GetFiles())
            {
                lstFiles.Add(new DownloadReportInfo()
                {
                    FileName = item.Name,
                    FilePath = dirInfo.FullName + @"\" + item.Name
                });
            }
            return lstFiles;
        }
    }
}