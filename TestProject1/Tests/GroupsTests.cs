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
    public class GroupsTest<TypedWebDriver> where TypedWebDriver : IWebDriver, new()
    {
        private ControllersManager? _manager;
        private ControllersManager Manager
        {
            get { if (_manager == null) throw new NullReferenceException("Manager is null"); else return _manager; }
            set { _manager = value; }
        }

        [SetUp]
        public void SetupTest()
        {
            Manager = new ControllersManager(new TypedWebDriver());
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                Manager.Dispose();
            }
            catch (Exception)
            {
            }
        }

        [Test]
        [Description("Add new group")]
        public void AddGroupTest()
        {
            Manager.Scenarios.AddGroup(CreateGroupData.Random());
        }

        [Test]
        [Description("Edit group")]
        public void EditGroupTest()
        {
            Manager.Scenarios.EditGroup();
        }

        [Test]
        [Description("Remove group")]
        public void RemoveGroupTest()
        {
            Manager.Scenarios.RemoveGroup();
        }
    }
}