using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Paradijs.Core
{
    public class Product
    {
        #region properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ingredients { get; set; }
        public int Price { get; set; }

        public List<Review> Reviews { get; set; }
        public List<Order> Orders { get; set; }

        #endregion

        #region Constructers
        public Product()
        {

        }

        #endregion

        #region Methodes
      

        #endregion

    }
}