using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BLL;
using DAL;


namespace Samir.Web.Controllers
{
    /*Repository Pattern */
    //// Generic repository //// kan gebruikt worden om de coden te verkorten ==> Maak een generic repository voor de code die zelfde is in de database klasses
    //// Using repository can help insulate the app from changes in the data store ////



    public class ProductController : Controller
    {
        private ProductLogic pLogic = new ProductLogic();

        // GET: Product
        [HttpGet]
        public ActionResult AddProduct()
        {

            return View();
        }


        [HttpPost]
        public ActionResult AddProduct(Product product)
        {

            if (Session["User"] != null)
            {
                Product NewProduct = new Product();

                NewProduct = pLogic.AddProduct(product);
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
            model = pLogic.GetAllproduct();
            Session["Product"] = model;
            return View(model);

        }
        public ActionResult ViewProductDetails(int id)
        {
            Product model = new Product();
            model = pLogic.GetProductDetails(id);
            Session["Product"] = model;
            return View(model);

        }

        public ActionResult MyProducts()
        {
            List<Product> AllProducts = new List<Product>();

            AllProducts = pLogic.GetAllproduct();
            Session["Product"] = AllProducts;
            return View(AllProducts);
        }
    }
}