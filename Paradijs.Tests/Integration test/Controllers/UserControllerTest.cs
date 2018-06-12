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

        private UserLogic ULogic;
        private User user;
        
        [TestInitialize]
        public void TestInitialize()
        {
            ULogic = new UserLogic();
            user = new User
            {
                Id = 1,
                Email = "wafaa-obeido@hotmail.com",
                IsEmailVerified = false,
                Password = "123456",
                ConfirmPassword = "123456",
                Mobile = 0684522582,
                RememberMe = true,
            };
            ULogic.AddUser(user);
        }

        [TestMethod]
        public void TestUserToevoegen()
        {
            // Arrange
            int aantal = ULogic.AllUsers().Count();
            user = new User
            {
                Id = 1,
                Email = "safaa-obeido@hotmail.com",
                IsEmailVerified = false,
                Password = "123456",
                ConfirmPassword = "123456",
                Mobile = 0684522582,
                RememberMe = true,
            };
            
            // Act
            ULogic.AddUser(user);

            // Assert
            Assert.AreEqual(aantal + 1, ULogic.AllUsers().Count());
        }

        [TestMethod]
        public void Verified()
        {
            // Arrange
            bool actual = user.IsEmailVerified;


            // Act
            bool expected = ULogic.IsValidation(user);

            // Assert
            Assert.AreNotEqual(expected, actual);
        }


        [TestMethod]
        public void LogIn()
        {
            // Arrangg

            // Act
            User user2 = ULogic.LogIn(user);
            //ULogic.IsValidation(user2);

            // Assert
            Assert.AreEqual(user2, user );
        }


    }
}
