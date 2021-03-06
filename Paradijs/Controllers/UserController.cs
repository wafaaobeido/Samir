﻿using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using BLL;

using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Samir.Controllers
{

    public class UserController : Controller
    {

        private UserLogic ULogic;

        public UserController(UserLogic ulogic)
        {
            this.ULogic = ulogic;
        }
        public UserController()
        {
            this.ULogic = new UserLogic();
        }

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
                var IsExist = ULogic.CheckEmail(user);

                if (IsExist)
                {
                    ModelState.AddModelError("EmailExist", "Email Already exist");
                    ModelState.Clear();
                    return View(user);
                }

                user.ActivationCode = Guid.NewGuid();
                ULogic.PasswordHashing(user);

                // Save data to Database
                newuser = ULogic.AddUser(user);
                Session["User"] = newuser;

                // Send Email to User
                var verifyUrl = "/user/VerifyAccount/" + user.ActivationCode;
                var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
                ULogic.SendVerificationLinkEmail(user.Email, user.ActivationCode.ToString(), link);

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

        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            User user = new User();

            bool Status = false;
            user.ActivationCode = new Guid(id);
            var IsExist = ULogic.CheckActivationCode(user);
            if (IsExist)
            {
                user.IsEmailVerified = true;
                ULogic.IsValidation(user);
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
            bool Status = false;


            if (ULogic.Checkaccount(user))
            {
                user = ULogic.LogIn(user);
                if (user.IsEmailVerified == true)
                {
                    message = "U bent ingelogd";
                    Response.Cookies.Add(ULogic.RememberMe(user));
                    Session["Useremail"] = user.Email;
                    Session["User"] = user;
                    ViewBag.Message = message;
                    ViewBag.Status = true;
                    return RedirectToAction("ViewProducts", "Product");
                }
                message = "U account is nog niet geverifieerd, verifieer u account alstublieft en probeer nog en keer";

                ViewBag.Status = false;
                ViewBag.Message = message;
                ModelState.Clear();
                return View();
            }
            message = "De gebruikersnaam of wachtwoord is niet juist, controleer uw gegevens alstublieft en probeer nog en keer";


            ViewBag.Status = false;
            ViewBag.Message = message;
            ModelState.Clear();
            return View();
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


        //public ActionResult AllUsers()
        //{

        //    if (Session["User"] != null)
        //    {
        //        List<User> model = new List<User>();

        //        model = ULogic.AllUsers();
        //        return View(model);

        //    }
        //    return RedirectToAction("Login", "User");
        //}

        public ActionResult DeleteUser(int id)
        {
            ULogic.DeleteUser(id);
            return RedirectToAction("AllUsers", "User");
        }

        public int testm()
        {
            int i = 1;
            int o = 1;
            return i + o;
        }



    }
}