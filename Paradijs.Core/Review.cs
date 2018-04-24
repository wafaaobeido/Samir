using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Paradijs.Core
{
    public class Review
    {
        #region Properties
        public int Id { get; set; }
        public User Buyer { get; set; }
        public string Content { get; set; }
        public DateTime PublishingTime { get; set; }
        public Product Product { get; set; }
        #endregion

        #region Constructers
        public Review()
        {

        }

        #endregion

        #region Methodes
     
        #endregion

    }
}