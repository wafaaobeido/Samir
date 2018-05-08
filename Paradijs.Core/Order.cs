﻿using System;
using System.Collections.Generic;
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
        public DateTime Ordertime { get; set; }
        public DateTime DeliveryTime { get; set; }
        public List<Product> Products { get; set; }


        #endregion

        #region Constructers
        public Order()
        {
            Ordertime = DateTime.Now;
        }

        #endregion

        #region Methodes

       

        #endregion

    }
}