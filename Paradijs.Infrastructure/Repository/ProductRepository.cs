using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public class ProductRepository
    {
        private IProduct context;

        public ProductRepository(IProduct context)
        {
            this.context = context;
        }
        public Product AddProduct(Product product)
        {
            return context.AddProduct(product);
        }
        public List<Product> GetAllproduct()
        {
            return context.All();
        }
        public Product GetProductDetails(int id)
        {
            return context.Details(id);
        }
        public Product ByID(int id)
        {
            return context.ByID(id);
        }
    }
}
