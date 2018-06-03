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

        public ProductRepository repo = new ProductRepository(new ProductSQLContext());

        public Product AddProduct(Product product)
        {
            return repo.AddProduct(product);
        }

        public List<Product> GetAllproduct()
        {
            return repo.GetAllproduct();
        }

        public Product GetProductDetails(int id)
        {
            return repo.GetProductDetails(id);
        }
        public Product ByID(int id)
        {
            return repo.ByID(id);
        }


    }
}
