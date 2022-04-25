using System;
using System.Collections.Generic;
using System.Threading;
using AddressBookAutotests.Controllers;
using AddressBookAutotests.Models;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace AddressBookAutotests.Dev
{
    [TestFixture()]
    public class DevTest
    {
        private ControllersManager? _manager;
        private ControllersManager Manager
        {
            get { if (_manager == null) throw new NullReferenceException("Manager is null"); else return _manager; }
            set { _manager = value; }
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {

            }
            catch (Exception)
            {
            }
        }

        [SetUp]
        public void Method()
        {
            Manager = ControllersManager.GetInstance();
        }

        [Test]
        [Description("")]
        public void Test()
        {
            Method();
        }
    }
}
