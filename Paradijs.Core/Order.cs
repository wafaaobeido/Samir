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
        //[Display(Name = "naam Klant")]
        public User User { get; set; }
        //[Display(Name = "naam Verkoper")]
        public User verkoper { get; set; }
        //[Display(Name = "Order time")]
        public DateTime Ordertime { get; set; }
        //[Display(Name = "Delivery time")]
        public DateTime DeliveryTime { get; set; }
        //[Display(Name = "naam Product")]
        public Product product { get; set; }
        public List<Product> Products { get; set; }


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