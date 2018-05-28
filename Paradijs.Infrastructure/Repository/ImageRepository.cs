using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public class ImageRepository
    {
        private IImage context;
        public ImageRepository (IImage context)
        {
            this.context = context;
        }
        public Image AddImage (Image image )
        {
            return context.AddImage(image);
        }
    }
}
