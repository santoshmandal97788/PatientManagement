using PatientManagement.Models;
using PatientManagement.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PatientManagement.Controllers
{
    public class TestController : Controller
    {

        HospitalDBEntities db = new HospitalDBEntities();


        // GET: Test
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CamTest()
        {
            return View();
        }
        public string UploadWebCamImage(string imageData)
        {
            string filename = Server.MapPath("~/UploadWebcamImages/webcam_") + DateTime.Now.ToString().Replace("/", "-").Replace(" ", "_").Replace(":", "") + ".png";
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(imageData);
                    bw.Write(data);
                    bw.Close();
                }
            }
            return "success";
        }
        //[HttpPost]
        //public ActionResult Create(string Imagename)
        //{
        //    ViewBag.pic = "http://localhost:44354/WebImages/" + Session["val"].ToString();
        //    return View();
        //}

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "PatImage")] PatientViewModel pvm)
        {

            //if (File1 != null)
            //{
            //    pvm.PatImage = new byte[File1.ContentLength]; // file1 to store image in binary formate  
            //    File1.InputStream.Read(pvm.PatImage, 0, File1.ContentLength);


            //}

            byte[] imageData = null;
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase objFiles = Request.Files["PatImage"];

                using (var binaryReader = new BinaryReader(objFiles.InputStream))
                {
                    imageData = binaryReader.ReadBytes(objFiles.ContentLength);
                }
            }

            db.spCreatePatient(imageData, pvm.PatName, pvm.PatContact, pvm.IsActive, DateTime.Now);
            return View();
        }

    }
}