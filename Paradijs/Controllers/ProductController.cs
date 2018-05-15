using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using DAL;


namespace Samir.Web.Controllers
{
    /*Repository Pattern */
    //// Generic repository //// kan gebruikt worden om de coden te verkorten ==> Maak een generic repository voor de code die zelfde is in de database klasses
    //// Using repository can help insulate the app from changes in the data store ////



    public class ProductController : Controller
    {
        // private IProduct db = new ProductSQLContext();
        private ProductRepository repo = new ProductRepository(new ProductSQLContext());

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

                NewProduct = repo.AddProduct(product);
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
           
            model = repo.GetAllproduct();
            Session["Product"] = model;
            return View(model);

        }
        public ActionResult ViewProductDetails(int id)
        {
            List<Product> model = new List<Product>();
           
            model = repo.GetProductDetails(id);
            Session["Product"] = model;
            return View(model);

        }
    }
}