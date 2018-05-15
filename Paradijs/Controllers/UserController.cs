using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using DAL;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Samir.Controllers
{

    public class UserController : Controller
    {
        //private IUser _database = new UserSQLContext();
        private UserRepository repo = new UserRepository(new UserSQLContext());
        /* Registration Action */ 

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Exclude = "IsEmailVerified,ActivationCode")] User user)
        {
            bool Status = false;
            string message = "";
            User newuser = new User();

            // Model validation
            if (ModelState.IsValid)
            {
            
                // Email is already Exist
                var IsExist = repo.CheckEmail(user);
                if (IsExist)
                {
                    ModelState.AddModelError("EmailExist", "Email Already exist");
                    ModelState.Clear();
                    return View(user);
                }
                user.ActivationCode = Guid.NewGuid();

                // Password Hashing
                user.Password = Crypto.Hash(user.Password);
                user.ConfirmPassword = Crypto.Hash(user.ConfirmPassword);
                user.IsEmailVerified = false;

                // Save data to Database
                newuser = repo.AddUser(user);
                Session["User"] = newuser;

                // Send Email to User
                SendVerificationLinkEmail(user.Email, user.ActivationCode.ToString());
                message = "Registration successfully done. Account activation link" + 
                    " has been sent to your Email:" + user.Email;
                Status = true;
            }
            else
            {
                message = "Invalid Request";
            }

            ViewBag.Message = message;
            ViewBag.Status = Status;
            ModelState.Clear();
            return View();
        }


        /* Verify Account */
        [NonAction]
        public void SendVerificationLinkEmail(string EmaiID, string ActivationCode)
        {
            var verifyUrl = "/user/VerifyAccount/" + ActivationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            var FromEmail = new MailAddress("samirobeido76@gmail.com", "Samir Obeido");
            var ToEmail = new MailAddress(EmaiID);
            var FromEmailPass = "sawsan@1968";
            string subject = "Your account is successfully created!";

            string Content = "</br></br>We are excited to tell you that your account is" +
                "successfully created. Please click on the below link to verify your account" +
                "</br></br><a href='" + link + "'>" + link + "</a>";
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(FromEmail.Address, FromEmailPass)

            };
            using (var message = new MailMessage(FromEmail, ToEmail)
            {
                Subject = subject,
                Body = Content,
                IsBodyHtml = true

            })

                smtp.Send(message);

        }

        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            User user = new User();
            
            bool Status = false;
            user.ActivationCode = new Guid(id);
            var IsExist = repo.CheckActivationCode(user);
            if (IsExist)
            {
                user.IsEmailVerified = true;
                repo.IsValidation(user);
                Status = true;
            }
            else
            {
                ViewBag.Message = "Invalid Request";
            }

            ViewBag.Status = Status;
            return View();
        }

        /* LogIn Action */
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            

            string message = "";
            user = repo.LogIn(user);
            if (user.IsEmailVerified == true)
            {
                if (user.Email != "" && Session["Useremail"] == null)
                {
                    int timeout = user.RememberMe ? 525600 : 20; // 525600 min = 1 year
                    var ticket = new FormsAuthenticationTicket(user.Email, user.RememberMe, timeout);
                    string Encrypted = FormsAuthentication.Encrypt(ticket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, Encrypted);
                    cookie.Expires = DateTime.Now.AddMinutes(timeout);
                    cookie.HttpOnly = true;
                    Response.Cookies.Add(cookie);
                    Session["Useremail"] = user.Email;
                    Session["User"] = user;
                    ModelState.Clear();
                    return RedirectToAction("ViewProducts", "Product");
                }
                else
                {
                    message = "Invalid Credential provider";
                }

                ViewBag.Message = message;
                ModelState.Clear();
                return View();
            }
            return RedirectToAction("Login");
        }

        /* Logout */
        [HttpPost]
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["Useremail"] = null;
            return RedirectToAction("ViewProducts", "Product");
        }
    }
}