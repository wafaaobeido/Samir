using Paradijs.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paradijs.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        [HttpGet]
        public ActionResult AddProduct()
        {

            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
            Image image = new Image();
            string filename = Path.GetFileNameWithoutExtension(image.ImageFile.FileName);
            string extention = Path.GetExtension(image.ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extention;
            image.ImagePath = "~/Images/" + filename;
            filename = Path.Combine(Server.MapPath("~/Images/"), filename);
            image.ImageFile.SaveAs(filename);


            Product NewProduct = new Product();
            DB db = new DB();
            NewProduct = db.AddProduct(product);

                //string message = " Account succesvol aangemaakt U kunt nu inloggen.";
                //ViewBag.Message = message;
                ViewBag.Status = true;
                ModelState.Clear();
                return View(NewProduct);
          

            //bool message = true;
            //DB db = new DB();
            //message = db.AddProduct(product);
            //ViewBag.Message = message;
            //ViewBag.Status = true;
            //ModelState.Clear();

            //Session["Product"] = product;
            //return View(product);
        }


        public ActionResult ViewProducts()
        {
            List<Product> model = new List<Product>();
            DB db = new DB();
            model = db.ViewProducts();
            Session["Product"] = model;
            return View(model);

        }
        public ActionResult ViewProductDetails(int id)
        {
            List<Product> model = new List<Product>();
            DB db = new DB();
            model = db.ViewProductDetails(id);
            Session["Product"] = model;
            return View(model);

        }
    }
}