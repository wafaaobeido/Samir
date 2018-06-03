using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Models;

namespace BLL
{
    public class ImageLogic
    {
        private ImageRepository repo = new ImageRepository(new ImageSQLContext());
        public Image AddImage(Image image)
        {
            return repo.AddImage(image);
        }
    }
}
