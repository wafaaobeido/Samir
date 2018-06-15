using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Models
{
    public class Order 
    {
        #region Properties

        public int Id { get; set; }
        public User User { get; set; }
        public User verkoper { get; set; }
        public DateTime Ordertime { get; set; }
        public DateTime DeliveryTime { get; set; }
        public Product product { get; set; }


        #endregion

        #region Constructers
        public Order()
        {
            product = new Product();
            User = new User();
            verkoper = new User();
            Ordertime = DateTime.Now;
            DeliveryTime = Ordertime.AddMonths(1);
        }
 
        #endregion

       

    }
}