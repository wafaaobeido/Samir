using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public class MessageRepository
    {
        private IMessage context;

        public MessageRepository(IMessage context)
        {
            this.context = context;
        }
        public List<ViewModelMessages> MessageInbox(int id)
        {
            return context.MessageInbox(id);
        }
        public List<ViewModelMessages> MessageSent(int id)
        {
            return context.MessageSent(id);
        }
        public List<Message> MessagesForOneProduct(User recipient, User sender, Product product)
        {
            return context.MessagesForOneProduct(recipient, sender, product);
        }
        public void SendMessage(Message Message)
        {
            context.SendMessage(Message);
        }
        public List<ViewModelMessages> ViewAllMessages(User User)
        {
            return context.ViewAllMessages(User);
        }
    }
}
