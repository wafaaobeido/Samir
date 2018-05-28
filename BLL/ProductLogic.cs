using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Models;

namespace BLL
{
    public class ProductLogic
    {

        private ProductRepository repo;

        public Product AddProduct(Product product)
        {
            return repo.AddProduct(product);
        }

        public List<Product> GetAllproduct()
        {
            return repo.GetAllproduct();
        }

        public List<Product> GetProductDetails(int id)
        {
            return repo.GetProductDetails(id);
        }

    }
}
