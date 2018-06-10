using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using DAL;
using Models;

namespace BLL
{
    public class UserLogic : IUserLogic
    {
        public UserRepository repo = new UserRepository(new UserSQLContext());

        public bool CheckEmail(User user)
        {
            return repo.CheckEmail(user);
        }
        public bool Checkaccount(User user)
        {
            return repo.Checkaccount(user);
        }
        public User AddUser(User user)
        {
            return repo.AddUser(user);
        }
        public bool CheckActivationCode(User user)
        {
            return repo.CheckActivationCode(user);
        }
        public bool IsValidation(User user)
        {
            return repo.IsValidation(user);
        }
        public virtual User LogIn(User user)
        {
            return repo.LogIn(user);
        }

        public void DeleteUser(int id)
        {
            repo.DeleteUser(id);
        }
        public List<User> AllUsers()
        {
            return repo.AllUsers();
        }
        public void SendVerificationLinkEmail(string EmaiID, string ActivationCode, string link)
        {
            var FromEmail = new MailAddress("samirobeido76@gmail.com", "Samir Obeido");
            var ToEmail = new MailAddress(EmaiID);
            var FromEmailPass = "sawsan@1968";
            string subject = "Your account is successfully created!";

            string Content = "</br></br>We are excited to tell you that your account is " +
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
        public HttpCookie RememberMe(User user)
        {
            int timeout = user.RememberMe ? 525600 : 20; // 525600 min = 1 year
            var ticket = new FormsAuthenticationTicket(user.Email, user.RememberMe, timeout);
            string Encrypted = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, Encrypted);
            cookie.Expires = DateTime.Now.AddMinutes(timeout);
            cookie.HttpOnly = true;
            return cookie;
           
        }
        public void PasswordHashing(User user)
        {
            // Password Hashing
            user.Password = Utils.Hash(user.Password);
            user.ConfirmPassword = Utils.Hash(user.ConfirmPassword);
            user.IsEmailVerified = false;
        }

        public void SendMessage(Message Message)
        {
            repo.SendMessage(Message);
        }

        public List<Message> ViewAllMessages(User User)
        {
            return repo.ViewAllMessages(User);
        }
        public List<Message> MessagesForOneProduct(User recipient, User sender, Product product)
        {
            return repo.MessagesForOneProduct(recipient, sender, product);
        }
        public List<Message> MessageInbox(int id)
        {
            return repo.MessageIndex(id);
        }
        public List<Message> MessageSent(int id)
        {
            return repo.MessageSent(id);
        }
    }
}
