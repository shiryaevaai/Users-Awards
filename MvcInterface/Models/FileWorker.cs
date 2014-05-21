namespace MvcInterface.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;

    public class FileWorker
    {
        //private static string fileName;

        public static void SaveFile(HttpPostedFileBase file)
        {
            file.SaveAs(@"D:\myFile");
            FileName = file.FileName;
        }

        public static byte[] GetFile()
        {
            return File.ReadAllBytes(@"D:\myFile");
        }

        public static string FileName { get; private set; }
    }
}