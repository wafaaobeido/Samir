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

            Product NewProduct = new Product();
            ProductDB db = new ProductDB();
            NewProduct = db.AddProduct(product);
            Session["Product"] = NewProduct;
            return View(product);
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