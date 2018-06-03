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
            string message = "";

            Product product = PLogic.ByID(Convert.ToInt32(id));
            string MessageforRent = "Hallo, Ik zou graag uw gercht : " + product.Name + " willen bestellen. ";
            OLogic.StandardMessage(Convert.ToInt32(clientID), Convert.ToInt32(hostID), MessageforRent, product.Name, Convert.ToInt32(id));


            message = OLogic.AddOrder(Convert.ToInt32(clientID), Convert.ToInt32(hostID), Convert.ToInt32(id));
            if (message == "succes")
            {
                return View();
            }
            return View();
        }
    }
}