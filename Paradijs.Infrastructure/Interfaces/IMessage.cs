using System.Collections.Generic;
using Models;

namespace DAL
{
    public interface IMessage
    {
        List<ViewModelMessages> MessageInbox(int id);
        List<ViewModelMessages> MessageSent(int id);
        List<Message> MessagesForOneProduct(User recipient, User sender, Product product);
        void SendMessage(Message Message);
        List<ViewModelMessages> ViewAllMessages(User User);
    }
}