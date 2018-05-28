using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace DAL
{
    /// <summary>
    /// A collection of static methods to assist in retrieving information from the database or executing repetitive tasks. 
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// Hash a plain-text password so it can be safely stored in the database.
        /// </summary>
        /// <param name="value">The plain-text password to hash.</param>
        /// <returns></returns>
        public static string Hash(string value)
        {
            return Convert.ToBase64String(
                System.Security.Cryptography.SHA256.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(value))
                );
        }

        /// <summary>
        /// Populate an instance of <see cref="Product"/> from an <see cref="SqlDataReader"/>
        /// </summary>
        /// <param name="reader">The <see cref="SqlDataReader"/> containing a single record describing an <see cref="Product"/></param>
        /// <returns>An instance of <see cref="Product"/> populated with all available properties</returns>
        /// 
        public static Product ProductFromReader(SqlDataReader reader)
        {
            ProductSQLContext PContext = new ProductSQLContext();
            var ImageContext = new ImageSQLContext();
            Product newproduct = new Product
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = (string)reader["Name"],
                Ingredients = (string)reader["Ingredients"],
                Picture = ImageContext.GetImageForProduct(Convert.ToInt32(reader["Id"]))
            };

            return newproduct;
        }

        /// <summary>
        /// Populate an instance of <see cref="User"/> from an <see cref="SqlDataReader"/>
        /// </summary>
        /// <param name="reader">The <see cref="SqlDataReader"/> containing a single record describing an <see cref="User"/></param>
        /// <returns>An instance of <see cref="User"/> populated with all available properties</returns>
        /// 
        public static User UserFromReader(SqlDataReader reader)
        {
            ProductSQLContext UContext = new ProductSQLContext();
            User newuser = new User
            {
                Email = (string)reader["EmailID"],
                IsEmailVerified = (bool)reader["IsEmailVerified"]
            };

            return newuser;
        }
    }
}