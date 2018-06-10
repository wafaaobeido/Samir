using System.Collections.Generic;
using Models;

namespace DAL
{
    public interface IUserRepository
    {
        User AddUser(User user);
        List<User> AllUsers();
        bool Checkaccount(User user);
        bool CheckActivationCode(User user);
        bool CheckEmail(User user);
        void DeleteUser(int id);
        bool IsValidation(User user);
        User LogIn(User user);
        List<Message> MessageIndex(int id);
        List<Message> MessageSent(int id);
        List<Message> MessagesForOneProduct(User recipient, User sender, Product product);
        void SendMessage(Message Message);
        List<Message> ViewAllMessages(User User);
    }
}