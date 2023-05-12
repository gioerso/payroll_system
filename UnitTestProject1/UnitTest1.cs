using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Web.Mvc;
using WebApplication2.Controllers;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private AccountController controller;
        private ViewResult result;
        [TestInitialize]
        public void SetupContext()
        {
            controller = new AccountController();
            HomeController homeContr = new HomeController();
            result = homeContr.Index() as ViewResult;

        }
        [TestMethod]
        public void IndexViewResultNotNull()
        {
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void IndexViewEqualIndexCshtml()
        {
            Assert.AreEqual("", result.ViewName);
        }

    }
}
