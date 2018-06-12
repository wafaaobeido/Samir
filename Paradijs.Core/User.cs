using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Models
{
    public class User : IComparable<User>
    {
        #region Properties

        public int Id { get; set; }
        [Display(Name = "Voornaam")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "De voornaam moet nog ingevoerd worden.")]
        public string FirstName { get; set; }
        [Display(Name = "Achternaam")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "De achternaam moet nog ingevoerd worden.")]
        public string LastName { get; set; }
        [Display(Name = "Geboortedatum")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString ="{0:MM/dd/yyyy}")]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Adres")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Het adres moet nog ingevoerd worden.")]
        public string Adress { get; set; }
        [Display(Name = "Postcode")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "De postcode moet nog ingevoerd worden.")]
        public string Postcode { get; set; }
        [Display(Name = "Stad")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "De stad moet nog ingevoerd worden.")]
        public string City { get; set; }
        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "De email moet nog ingevoerd worden.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } 
        [Display(Name = "Wachtwoord")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Het wachtwoord moet nog ingevoerd worden.")]
        [DataType(dataType: DataType.Password)]
        [MinLength(6, ErrorMessage = "Wachtwoord moet minimaal 6 karakters lang zijn.")]
        public string Password { get; set; }
        [Display(Name="Bevestigte wachtwoord")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Wachtwoord en de bevestiging Komt niet overeen.")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Telefoon nummer")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Het ingevulde telefoonnummer is niet geldig.")]
        public int Mobile { get; set; }
        public bool IsEmailVerified { get; set; }
        public Guid ActivationCode { get; set; }
        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }

        public Order Order { get; set; }
        public List<Review> Reviews { get; set; }

        #endregion

        #region Constructers
        public User (Order o)
        {
            this.Order = o;
        }
        public User()
        {

        }

        #endregion

        public class UserNietGevondenException : Exception { }

        public int CompareTo(User other)
        {
            if (other != null)
            {
                return this.Email.CompareTo(other.Email);
            }
            return 1; 
        }

    }
}