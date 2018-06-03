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
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Klant moet ingevoerd worden!")]
        public User User { get; set; }
        //[Display(Name = "naam Verkoper")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Verkoper moet ingevoerd worden!")]
        public User verkoper { get; set; }
        //[Display(Name = "Order time")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Order time moet ingevoerd worden!")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Ordertime { get; set; }
        //[Display(Name = "Delivery time")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Delivery time moet ingevoerd worden!")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DeliveryTime { get; set; }
        //[Display(Name = "naam Product")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Product moet ingevoerd worden!")]
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