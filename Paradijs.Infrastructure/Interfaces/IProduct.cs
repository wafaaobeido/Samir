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
        List<Product> ViewProducts();
        List<Product> ViewProductDetails(int id);
        Product AddProduct(Product product);
        void DeleteProduct(Product product);
        void EditProduct(Product product);

    }
}
