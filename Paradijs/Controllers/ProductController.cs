using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using DAL;


namespace Samir.Web.Controllers
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
            ProductDB db = new ProductDB();
            if (Session["User"] != null)
            {
                Product NewProduct = new Product();


                NewProduct = db.AddProduct(product);
                if (NewProduct.Id != 0)
                {
                    Session["Product"] = NewProduct;
                    return RedirectToAction("AddImage", "Image", new { es = NewProduct });
                }
                return View(product);

            }
            return RedirectToAction("Login", "User");

        }


        public ActionResult ViewProducts()
        {
            List<Product> model = new List<Product>();
            ProductDB db = new ProductDB();
            model = db.ViewProducts();
            Session["Product"] = model;
            return View(model);

        }
        public ActionResult ViewProductDetails(int id)
        {
            List<Product> model = new List<Product>();
            ProductDB db = new ProductDB();
            model = db.ViewProductDetails(id);
            Session["Product"] = model;
            return View(model);

        }
    }
}