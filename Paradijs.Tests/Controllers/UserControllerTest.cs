using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Models;
using NUnit.Framework;
using Samir;
using Samir.Controllers;

namespace Samir.Tests.Controllers
{
    [TestFixture]
    public class UserControllerTest
    {
        [Test]
        public void Registration()
        {
            // Arrange
            User user = new User();
            UserController controller = new UserController();

            // Act
            ViewResult result = controller.Registration() as ViewResult;

            // Assert
            Assert.AreEqual("Registration successfully done. Account activation link" +
                    " has been sent to your Email:" + user.Email, result.ViewBag.Message);
        }

        
    }
}
