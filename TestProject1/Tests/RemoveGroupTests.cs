using System;
using AddressBookAutotests.Controllers;
using AddressBookAutotests.Models;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace AddressBookAutotests
{
    [TestFixture(typeof(FirefoxDriver))]
    [TestFixture(typeof(ChromeDriver))]
    public class RemoveGroupTests<TypedWebDriver> where TypedWebDriver : IWebDriver, new()
    {
        private ControllersManager? manager;

        [SetUp]
        public void SetupTest()
        {
            IWebDriver driver = new TypedWebDriver();
            manager = new ControllersManager(driver);
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                if (manager == null) throw new NullReferenceException("ControllersManager не может быть null");
                manager.Dispose();
            }
            catch (Exception)
            {
            }
        }

        public void RemoveGroup()
        {
            if (manager == null) throw new NullReferenceException("ControllersManager не может быть null");
            manager.Navigate.NavigateHomePage()
                .Authorization.Login(Auth.Admin)
                .Navigate.ToGroups()
                .Groups.RemoveGroup()
                .Authorization.Logout();
        }

        [Test]
        public void RemoveGroupTest()
        {
            RemoveGroup();
        }
    }
}
