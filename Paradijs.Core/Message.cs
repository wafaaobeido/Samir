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
        public int SenderID { get; set; }
        public int RecipientID { get; set; }
        public int ProductID { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
