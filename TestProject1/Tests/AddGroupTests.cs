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
    public class AddGroupTestCase<TypedWebDriver> where TypedWebDriver : IWebDriver, new()
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

        public void AddGroup(Group group)
        {
            if (manager == null) throw new NullReferenceException("ControllersManager не может быть null");
            manager.Navigate.NavigateHomePage()
                .Authorization.Login(Auth.Admin)
                .Navigate.ToGroups()
                .Navigate.AddNewGroup()
                .Groups.AddGroupFillFields(group)
                .Groups.AddGroupApply()
                .Authorization.Logout();
        }

        [Test]
        public void AddGroupTest()
        {
            AddGroup(Group.GetRandom());
        }
    }
}