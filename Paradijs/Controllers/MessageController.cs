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
        MessageLogic MLogic = new MessageLogic();

        [HttpGet]
        public ActionResult CreateMessage(int recipientid, int senderid, int productid)
        {
            Message Message = new Message();
            Message.Recipient.Id = recipientid;
            Message.Sender.Id = senderid;
            Message.Product.Id = productid;
            return View(Message);
        }

        [HttpPost]
        public ActionResult CreateMessage(Message message)
        {
            MLogic.SendMessage(message);
            return RedirectToAction("ViewProducts", "Product");
        }

        public ActionResult ViewAllMessages(int id)
        {
            User user = new User();
            user.Id = id;
            List<ViewModelMessages> messages = new List<ViewModelMessages>();
            messages = MLogic.ViewAllMessages(user);
            Session["Messages"] = messages;
            string mes = "All messages";
            ViewBag.Message = mes;
            return View(messages);

        }

        public ActionResult MessageSent(int id)
        {
            User user = new User();
            user.Id = id;
            foreach (var item in MLogic.ViewAllMessages(user))
            {
                if (item.Recipient.Id == user.Id)
                {
                    return View(MLogic.MessageSent(id));
                }
            }
            return RedirectToAction("ViewAllMessages", "User");
        }
        public ActionResult MessageInbox(int id)
        {
            User user = new User();
            user.Id = id;
            foreach (var item in MLogic.ViewAllMessages(user))
            {
                if (item.Sender.Id != user.Id)
                {
                    return View(MLogic.MessageInbox(id));
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
            return View(MLogic.MessagesForOneProduct(recipient, sender, product));
        }

    }
}