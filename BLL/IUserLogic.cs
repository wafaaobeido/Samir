using System.Collections.Generic;
using System.Web;
using Models;

namespace BLL
{
    public interface IUserLogic
    {
        User AddUser(User user);
        List<User> AllUsers();
        bool Checkaccount(User user);
        bool CheckActivationCode(User user);
        bool CheckEmail(User user);
        void DeleteUser(int id);
        bool IsValidation(User user);
        User LogIn(User user);
        void PasswordHashing(User user);
        HttpCookie RememberMe(User user);
        void SendVerificationLinkEmail(string EmaiID, string ActivationCode, string link);
    }
}