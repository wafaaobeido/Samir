using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IUser
    {
        bool IsEmailExists(User user);
        bool Checkaccount(User user);
        User AddUser(User user);
        bool IsActivationCodeExists(User user);
        bool IsValidation(User user);
        User LogIn(User user);
        List<User> AllUsers();
        void DeleteUser(int id);
        void EditUser();

    }
}
