using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using BLL;
using DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using Samir;
using Samir.Controllers;

namespace Samir.Tests.Controllers
{
    [TestClass]
    public class UserControllerTest
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public UserControllerTest()
        {
            // Some Mock Users


        }

        public void Arrange()
        {

        }

        [TestMethod]
        public void LogIn()
        {
           
            User user1 = new User {
                Id = 1,
                Email = "wafaa-obeido@hotmail.com",
                IsEmailVerified = false,
                Password = "123456",
                ConfirmPassword = "123456",
                Mobile = 0684522582,
                RememberMe = true,
            };

            User user2 = new User
            {
                Id = 1,
                Email = "wafaa-obeido@hotmail.com",
                IsEmailVerified = true,
                Password = "123456",
                ConfirmPassword = "123456",
                Mobile = 0684522582,
                RememberMe = true,
            };
            Mock<IUser> mock = new Mock<IUser>();

            mock.Setup(m => m.LogIn(user1)).Returns( user2);
            //var controller = new UserController();

            //ViewResult result = controller.Login(user1) as ViewResult;
            //var expetedmessage = "U bent ingelogd";

            Assert.AreEqual(user1.IsEmailVerified == false, mock.Object.LogIn(user1).IsEmailVerified == true);


        }

        [TestMethod]
        public void LogIn_Cintroller()
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
            UserController controller = new UserController(mock.Object);

            var  result = controller.Login(user1).Model;
            var expetedmessage = "U bent ingelogd";


            Assert.IsInstanceOfType(result,typeof(User));


        }


        [TestMethod]
        public void test_testm()
        {
            // Arrange

            UserController controller = new UserController();

            // Act
            var result = controller.testm();

            // Assert
            Assert.AreEqual(2, result);
        }


    }
}
