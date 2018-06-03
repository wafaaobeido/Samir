using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IProduct
    {
        List<Product> All();
        Product Details(int id);
        Product AddProduct(Product product);
        Product ByID(int id);
        void DeleteProduct(Product product);
        void EditProduct(Product product);

    }
}
