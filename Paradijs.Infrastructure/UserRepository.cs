using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public class UserRepository
    {
        private IUser context;
        public UserRepository(IUser context)
        {
            this.context = context;
        }
        public User AddUser(User user)
        {
            return context.AddUser(user);
        }
        public User LogIn (User user)
        {
            return context.LogIn(user);
        }
        public bool CheckEmail(User user)
        {
            return context.IsEmailExists(user);
        }
        public bool CheckActivationCode(User user)
        {
            return context.IsActivationCodeExists(user);
        }
        public bool IsValidation(User user)
        {

            return context.IsValidation(user);
        }
    }
}
