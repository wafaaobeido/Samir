using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Models;

namespace BLL
{
    public class MessageLogic
    {
        public MessageRepository repo = new MessageRepository(new MessageSQLContext());
 
        public List<ViewModelMessages> MessageInbox(int id)
        {
            return repo.MessageInbox(id);
        }
        public List<ViewModelMessages> MessageSent(int id)
        {
            return repo.MessageSent(id);
        }
        public List<Message> MessagesForOneProduct(User recipient, User sender, Product product)
        {
            return repo.MessagesForOneProduct(recipient, sender, product);
        }
        public void SendMessage(Message Message)
        {
            repo.SendMessage(Message);
        }
        public List<ViewModelMessages> ViewAllMessages(User User)
        {
            return repo.ViewAllMessages(User);
        }
    }
}
