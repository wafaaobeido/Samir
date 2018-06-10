using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ViewModelMessages
    {
        public Product Product { get; set; }
        public User Sender { get; set; }

        public User Recipient { get; set; }
        public int NumberOfMessages { get; set; }

        public ViewModelMessages()
        {
            Sender = new User();
            Recipient = new User();
            Product = new Product();
        }
    }
}
