using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Models;

namespace Samir.Web.Controllers
{
    public class MessageController : Controller
    {
        UserLogic ULogic = new UserLogic();

        [HttpGet]
        public ActionResult CreateMessage(int recipientid, int senderid, int productid)
        {
            Message Message = new Message();
            Message.Recipient = recipientid;
            Message.Sender = senderid;
            Message.ProductID = productid;
            return View(Message);
        }

        [HttpPost]
        public ActionResult CreateMessage(Message message)
        {
            ULogic.SendMessage(message);
            return RedirectToAction("ViewProducts", "Product");
        }

        public ActionResult ViewAllMessages(int id)
        {
            User user = new User();
            user.Id = id;
            return View(ULogic.ViewAllMessages(user));

        }

        public ActionResult MessageSent(int id)
        {
            User user = new User();
            user.Id = id;
            foreach (var item in ULogic.ViewAllMessages(user))
            {
                if (item.SenderID == user.Id)
                {
                    return View(ULogic.MessageSent(id));
                }
            }
            return RedirectToAction("ViewAllMessages", "User");
        }
        public ActionResult MessageInbox(int id)
        {
            User user = new User();
            user.Id = id;
            foreach (var item in ULogic.ViewAllMessages(user))
            {
                if (item.SenderID != user.Id)
                {
                    return View(ULogic.MessageInbox(id));
                }
            }
            return RedirectToAction("ViewAllMessages", "User");
        }

        public ActionResult OneConversation(int recipientid, int senderid, int productid)
        {
            User recipient = new User();
            User sender = new User();
            Product product = new Product();
            product.Id = productid;
            recipient.Id = recipientid;
            sender.Id = senderid;
            return View(ULogic.MessagesForOneProduct(recipient, sender, product));
        }

    }
}