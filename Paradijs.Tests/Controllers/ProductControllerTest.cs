using Microsoft.VisualStudio.TestTools.UnitTesting;
using Samir.Web.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using BLL;
using Models;

namespace Paradijs.Tests.Controllers
{
    [TestClass]
    public class ProductControllerTest
    {
  
        public void ShowProducts_Returns_Listproduct()
        {
        //    // Arange
        //    Mock<List<ProductLogic>> mock = new Mock<List<ProductLogic>>();

        //    mock.Setup(m => m.GetAllproduct()).Returns (new List<Product>
        //    {
        //       new Product{ Id = 1, Name ="Barazeq", Ingredients ="Salt",Price = 30 },
        //       new Product{ Id = 2, Name ="Baraz", Ingredients ="Bloem",Price = 20 },
        //       new Product{ Id = 3, Name ="Bar", Ingredients ="Eten",Price = 40 }
        //    }
        //    );

        //    ProductController controller = new ProductController(mock.Object);

        //    //Act
        //    ViewResult actuel = controller.ViewProducts() as ViewResult;


        //    //Assert
        //    Assert.IsNotNull("ViewProducts", actuel.ViewName);
        }
    }
}
