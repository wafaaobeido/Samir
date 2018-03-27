using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Paradijs.Models
{
    public class Image
    {
        public int Id { get; set; }
        public int ImageTitle { get; set; }
        public string ImagePath { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }

        public Image()
        {

        }
    }
}