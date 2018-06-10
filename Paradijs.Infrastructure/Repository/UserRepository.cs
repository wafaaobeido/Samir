using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public class UserRepository : IUserRepository
    {
        public IUser context;

        public UserRepository(IUser context)
        {
            this.context = context;
        }
        public User AddUser(User user)
        {
            return context.AddUser(user);
        }
        public virtual User LogIn (User user)
        {
            return context.LogIn(user);
        }
        public bool CheckEmail(User user)
        {
            return context.IsEmailExists(user);
        }
        public bool Checkaccount(User user)
        {
            return context.Checkaccount(user);
        }
        public bool CheckActivationCode(User user)
        {
            return context.IsActivationCodeExists(user);
        }
        public bool IsValidation(User user)
        {

            return context.IsValidation(user);
        }
        public List<User> AllUsers()
        {
            return context.AllUsers();
        }
        public void DeleteUser(int id)
        {
             context.DeleteUser(id);
        }
        public void SendMessage(Message Message)
        {
            context.SendMessage(Message);
        }

        public List<Message> ViewAllMessages(User User)
        {
            return context.ViewAllMessages(User);
        }
        public List<Message> MessagesForOneProduct(User recipient, User sender, Product product)
        {
            return context.MessagesForOneProduct(recipient, sender, product);
        }
        public List<Message> MessageIndex(int id)
        {
            return context.MessageIndex(id);
        }
        public List<Message> MessageSent(int id)
        {
            return context.MessageSent(id);
        }
    }
}
