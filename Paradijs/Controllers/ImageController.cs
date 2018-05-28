using System;
using System.IO;
using Models;
using BLL;
using System.Web.Mvc;


namespace Samir.Web.Controllers
{
    public class ImageController : Controller
    {
        private ImageLogic iLogic;

        // GET: Image
        [HttpGet]
        public ActionResult AddImage()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult AddImage([Bind()]Image image)
        {

            string filename = Path.GetFileNameWithoutExtension(image.ImageFile.FileName);
            string extention = Path.GetExtension(image.ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extention;
            image.ImagePath = "~/Images/" + filename;
            filename = Path.Combine(Server.MapPath("~/Images/"), filename);
            image.ImageFile.SaveAs(filename);
            

            image = iLogic.AddImage(image);
            Session["Image"] = image;

            ModelState.Clear();
            return View(image);

        }

    }
}