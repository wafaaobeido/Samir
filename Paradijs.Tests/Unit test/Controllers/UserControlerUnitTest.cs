using BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using Samir.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paradijs.Tests.Unit_test.Controllers
{
    [TestClass]
    public class UserControlerUnitTest
    {


        [TestMethod]
        public void LogIn_Controller()
        {
            var user1 = new User
            {
                Id = 1,
                Email = "wafaa-obeido@hotmail.com",
                IsEmailVerified = false,
                Password = "123456",
                ConfirmPassword = "123456",
                Mobile = 0684522582,
                RememberMe = true,

            };
            var user2 = new User
            {
                Id = 1,
                Email = "wafaa-obeido@hotmail.com",
                IsEmailVerified = true,
                Password = "123456",
                ConfirmPassword = "123456",
                Mobile = 0684522582,
                RememberMe = true,
            };
            Mock<IUserLogic> mock = new Mock<IUserLogic>();
            mock.Setup(m => m.LogIn(user1)).Returns(user2);

            Assert.AreEqual(user1.IsEmailVerified == false, mock.Object.LogIn(user1).IsEmailVerified == true);
        }

    }
}
