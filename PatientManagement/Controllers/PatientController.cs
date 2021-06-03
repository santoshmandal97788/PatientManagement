using PatientManagement.Models;
using PatientManagement.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PatientManagement.Controllers
{
    public class PatientController : Controller
    {
        HospitalDBEntities db = new HospitalDBEntities();
        // GET: Patient
        public ActionResult Index()
        {
            List<PatientViewModel> lstabt = new List<PatientViewModel>();

            var list = db.spGetAllPatient().ToList();
            foreach (var item in list)
            {
                lstabt.Add(new PatientViewModel() { Id=item.Id, PatImage = item.PatImage, PatContact = item.PatContact, IsActive = item.IsActive, PatName = item.PatName });

            }

            return View(lstabt);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind(Exclude = "PatImage")] PatientViewModel pvm)
        {
            byte[] imageData = ConvertImage();

            db.spCreatePatient(imageData, pvm.PatName, pvm.PatContact, pvm.IsActive, DateTime.Now);
            return RedirectToAction("Index", "Patient");
        }


       
        private byte[] ConvertImage()
        {
            byte[] imageData = null;
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase pf = Request.Files["PatImage"];
                System.Drawing.Image bm = System.Drawing.Image.FromStream(pf.InputStream);
                bm = ResizeImage((Bitmap)bm, 98, 118); /// new width, heig

                imageData = imageToByteArray(bm);
                //using (var binaryReader = new BinaryReader(objFiles.InputStream))
                //{
                //    imageData = binaryReader.ReadBytes(objFiles.ContentLength);

                //}

            }

            return imageData;
        }

        public ActionResult Edit(int id)
        {
            // PatientViewModel pvm = new PatientViewModel();
            spGetPatientById_Result spGetPatientById_Result = db.spGetPatientById(id).FirstOrDefault();
           
            return View(spGetPatientById_Result);
        }
        [HttpPost]
        public ActionResult Edit([Bind(Exclude = "PatImage")] PatientViewModel pvm)
        {
            byte[] imageData = ConvertImage();

           
            var patient = db.spGetPatientById(pvm.Id).FirstOrDefault();
            db.spUpdatePatient(pvm.Id,imageData, pvm.PatName, pvm.PatContact, pvm.IsActive, patient.EntryDate);
            return RedirectToAction("Index", "Patient");
        }
        public ActionResult Delete(int id)
        {

            spGetPatientById_Result spGetPatientById_Result = db.spGetPatientById(id).FirstOrDefault();

            return View(spGetPatientById_Result);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeletePost(int id)
        {

            db.spDeletePatient(id);

            return RedirectToAction("Index", "Patient"); 
        }

        public ActionResult Details(int id)
        {

            spGetPatientById_Result spGetPatientById_Result = db.spGetPatientById(id).FirstOrDefault();

            return View(spGetPatientById_Result);
        }
        public ActionResult Print(int id)
        {

            spGetPatientById_Result spGetPatientById_Result = db.spGetPatientById(id).FirstOrDefault();

            return View(spGetPatientById_Result);
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }


        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

    }
}