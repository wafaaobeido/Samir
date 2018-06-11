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
        public IUser context;


        public UserRepository(RepositoryType type)
        {
            switch (type)
            {
                case RepositoryType.Geheugen:
                    context = new UserMemoryContext(); break;
                case RepositoryType.Database:
                    context = new UserSQLContext(); break;
                default:
                    throw new NotImplementedException($"{type} niet ondersteund voor deze repository.");
            }
        }

        public User AddUser(User user) => context.AddUser(user);
        public virtual User LogIn (User user) => context.LogIn(user);
        public bool CheckEmail(User user) => context.IsEmailExists(user);
        public bool Checkaccount(User user) => context.Checkaccount(user);
        public bool CheckActivationCode(User user) => context.IsActivationCodeExists(user);
        public bool IsValidation(User user) => context.IsValidation(user);
        public List<User> AllUsers() => context.AllUsers();
        public void DeleteUser(int id) => context.DeleteUser(id);

    }
}
