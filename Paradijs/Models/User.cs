using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Paradijs.Models
{
    public class User
    {
        #region Properties

        public int Id { get; set; }
        [Display(Name = "Voornaam")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Voornaam moet ingevoerd worden!")]
        public string FirstName { get; set; }
        [Display(Name = "Achternaam")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Achternaam moet ingevoerd worden!")]
        public string LastName { get; set; }
        public string Utype { get; set; }
        [Display(Name = "Geboortedatum")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Geboortedatum moet ingevoerd worden!")]
        public DateTime Birthday { get; set; }
        [Display(Name = "Adres")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Adres moet ingevoerd worden!")]
        public string Adress { get; set; }
        [Display(Name = "Postcode")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Postcode moet ingevoerd worden!")]
        public string Postcode { get; set; }
        [Display(Name = "Stad")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Stad moet ingevoerd worden!")]
        public string City { get; set; }
        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email moet ingevoerd worden!")]
        public string Email { get; set; }
        [Display(Name = "Wachtwoord")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Wachtwoord moet ingevoerd worden!")]
        [DataType(dataType: DataType.Password)]
        [MinLength(8, ErrorMessage = "Wachtwoord moet minimaal 8 karakters lang zijn.")]
        public string Password { get; set; }
        [Display(Name="Bevestigte wachtwoord")]
        [DataType(DataType.Password)]
        [Compare("Wachtwoord",ErrorMessage ="Wachtwoord en de bevestiging Komt niet overeen.")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Telefoon nummer")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Telefoon nummer moet ingevoerd worden!")]
        public int Mobile { get; set; }

        public List<Order> Orders { get; set; }
        public List<Review> Reviews { get; set; }

        #endregion

        #region Constructers
        public User ()
        {

        }

        #endregion

        #region Methodes

        #endregion

    }
}