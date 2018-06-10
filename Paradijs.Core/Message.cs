using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Message
    {
        public int Id { get; set; }
        // change to user 
        public User Sender { get; set; }
        public User Recipient { get; set; }
        public Product Product { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public int NumberOfMessages { get; set; }

        public Message()
        {
            Sender = new User();
            Recipient = new User();
            Product = new Product();
        }
    }
}
