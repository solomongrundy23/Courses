using System;
using System.Collections.Generic;
using System.Threading;
using AddressBookAutotests.Controllers;
using AddressBookAutotests.Models;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace AddressBookAutotests
{
    //[TestFixture(typeof(FirefoxDriver))]
    [TestFixture(typeof(ChromeDriver))]
    public class DevTest<TypedWebDriver> where TypedWebDriver : IWebDriver, new()
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
                Manager.Dispose();
            }
            catch (Exception)
            {
            }
        }

        public void Method()
        {
            Thread.Sleep(1);
        }

        [Test]
        [Description("")]
        public void Test()
        {
            Method();
        }
    }
}
