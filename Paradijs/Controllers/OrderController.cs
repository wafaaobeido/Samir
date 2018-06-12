using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BLL;

namespace Samir.Web.Controllers
{
    public class OrderController : Controller
    {
        private OrderLogic OLogic = new OrderLogic();
        private ProductLogic PLogic = new ProductLogic();
        private UserLogic ULogic = new UserLogic();
        // GET: Order
        [HttpGet]
        public ActionResult AddOrder(int? userid, int? id, int? hostid, Order c)
        {
                c = OLogic.GetInfo(Convert.ToInt32(userid), Convert.ToInt32(id), Convert.ToInt32(hostid));
                return View(c);
        }

        [HttpPost]
        public ActionResult AddOrder(int? id)
        {
            var clientID = Request.Form["User.Id"];
            var hostID = Request.Form["Verkoper.Id"];
            var quantity = Request.Params["Quantity"];

            string message = "";

            Product product = PLogic.ByID(Convert.ToInt32(id));
            message = OLogic.AddOrder(Convert.ToInt32(clientID), Convert.ToInt32(hostID), Convert.ToInt32(id), Convert.ToInt32(quantity)) +
                OLogic.KoppelTabelOrder(Convert.ToInt32(clientID), Convert.ToInt32(hostID), Convert.ToInt32(id), Convert.ToInt32(quantity));

            if (message == "succes")
            {
                Session["Order"] = message;
                return RedirectToAction("ViewProducts", "Product");
            }
            return View();
        }

        [HttpGet]
        public ActionResult MyOrders(int id)
        {
            if (Session["User"] != null)
            {
                List<Order> model = new List<Order>();
                

                model = OLogic.ShowOrder(id);
                return View(model);

            }
            return RedirectToAction("Login", "User");
        }

        public ActionResult AllUsers(int id)
        {

            if (Session["User"] != null)
            {
                List<User> model = new List<User>();

                model = ULogic.AllUsers();
                return View(model);

            }
            return RedirectToAction("Login", "User");
        }

        [HttpGet]
        public ActionResult OrdersByUser()
        {
            return View(OLogic.OrdersByUsers());
        }
    }
}