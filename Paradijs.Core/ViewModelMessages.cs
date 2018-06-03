using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ViewModelMessages
    {
        public int Productid { get; set; }
        public int SenderID { get; set; }

        public int RecipientID { get; set; }
        public int NumberOfMessages { get; set; }
    }
}
