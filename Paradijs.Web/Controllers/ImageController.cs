using System;
using System.IO;
using Paradijs.Core;
using System.Web.Mvc;
using Paradijs.Models;


namespace Paradijs.Web.Controllers
{
    public class ImageController : Controller
    {
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

            //Image newimage = new Image();
            DB db = new DB();

            image = db.AddImage(image);
            Session["Image"] = image;

            ModelState.Clear();
            return View(image);

            // when i add a image the product will not in de database is.
        }
    }
}