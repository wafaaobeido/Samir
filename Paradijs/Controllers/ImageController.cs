using System;
using System.IO;
using Models;
using DAL;
using System.Web.Mvc;


namespace Samir.Web.Controllers
{
    public class ImageController : Controller
    {
       // private IImage db = new ImageSQLContext();
        private ImageRepository repo = new ImageRepository(new ImageSQLContext());

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
            

            image = repo.AddImage(image);
            Session["Image"] = image;

            ModelState.Clear();
            return View(image);

            // when i add a image the product will not in de database is.
        }

    }
}