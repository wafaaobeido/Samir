using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Models
{
    public class Product
    {
        #region properties
        public int Id { get; set; }
        [Display(Name= "Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "De naam van de gerecht moet nog ingevoerd worden.")]
        public string Name { get; set; }
        [Display(Name = "Ingredients")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "De Ingredients moet nog ingevoerd worden.")]
        public string Ingredients { get; set; }
        [Display(Name = "Price")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "De Price moet nog ingevoerd worden.")]
        public decimal Price { get; set; }
        [Display(Name = "Photo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Kies tenminste ��n afbeelding")]
        public List<string> Picture { get; set; }
        [Display(Name = "Reviews")]
        public List<Review> Reviews { get; set; }
        [Display(Name = "Orders")]
        public List<Order> Orders { get; set; }

        #endregion

        #region Constructers
        public Product()
        {

        }

        #endregion

    

    }
}