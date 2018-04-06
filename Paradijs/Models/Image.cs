using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Paradijs.Models
{
    public class Image
    {
        public int Id { get; set; }
        [Display(Name = "Image tilte")]
        public int ImageTitle { get; set; }
        [Display(Name = "Upload Photo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Beeld moet upgeload worden!")]
        public string ImagePath { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }

        public Image()
        {

        }
    }
}