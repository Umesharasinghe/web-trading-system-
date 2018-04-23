using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace NewWebTrader.Models
{
    public class DownloadFile
    {
        public List<DownFileInfo> GetFiles()
        {
            List<DownFileInfo> lstFiles = new List<DownFileInfo>();
            DirectoryInfo dirInfo = new DirectoryInfo(HostingEnvironment.MapPath("~/uploads"));

            int i = 0;
            foreach (var item in dirInfo.GetFiles())
            {
                lstFiles.Add(new DownFileInfo()
                {

                    FileId = i + 1,
                    FileName = item.Name,
                    FilePath = dirInfo.FullName + @"\" + item.Name
                });
                i = i + 1;
            }
            return lstFiles;
        }
    }
}