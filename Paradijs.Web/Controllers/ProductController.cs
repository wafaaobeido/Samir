using Paradijs.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Paradijs.Web.Controllers
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
            DB db = new DB();
            NewProduct = db.AddProduct(product);
            Session["Product"] = NewProduct;
            return View(product);
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