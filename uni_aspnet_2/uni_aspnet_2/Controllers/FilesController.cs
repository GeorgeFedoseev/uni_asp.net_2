using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace uni_asp.net_2.Controllers
{
    public class FilesController : Controller
    {
        //
        // GET: /Files/

        public ActionResult Index() {          
            var files = System.IO.Directory.GetFiles(Server.MapPath("~") + "App_Data/Files/", "*");
            return View(files);
        }

        public ActionResult NotFound() {
            return new HttpNotFoundResult();
        }

        private string getFilePath(string filename){
            return Server.MapPath("~") + "App_Data/Files/"+filename;
        }

        public ActionResult content(string filename) {
            var path = getFilePath(filename);

            byte[] fileData;
            using (FileStream fs = new FileStream(path, FileMode.Open)) {
                MemoryStream ms = new MemoryStream();
                fs.CopyTo(ms);
                fileData = ms.ToArray();
            }

            return File(fileData, "text");
        }

        public string path(string filename) {
            var path = getFilePath(filename);
            if (System.IO.File.Exists(path)) {
                return path;
            }else{
                return "Not Found";
            }            
        }

        public ActionResult stream(string filename) {
            var path = getFilePath(filename);
            return File(new FileStream(path, FileMode.Open), "text");
        }

    }
}
