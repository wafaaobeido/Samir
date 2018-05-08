using System;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    interface IImage
    {
        Image AddImage(Image image);
        List<string> GetImageForProduct(int id);
    }
}
