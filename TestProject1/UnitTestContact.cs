using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Text;

namespace TestProject1
{
    [TestFixture(typeof(FirefoxDriver))]
    [TestFixture(typeof(ChromeDriver))]
    public class UnitTestContact<TypedWebDriver> where TypedWebDriver : IWebDriver, new()
    {
        private IWebDriver? driver;
        private StringBuilder? verificationErrors;
        private bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            driver = new TypedWebDriver();
            driver.Manage().Window.Maximize();
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        public void AddNewContact(bool group)
        {
            var methods = new Methods(driver);
            var contact = Contact.Random;
            if (group) contact.New_group = "Manager";
            methods.NavigateHomePage().Auth(Auth.Admin).AddContact(contact).Logout();
        }

        [Test]
        public void AddNewContactWithoutGroupTest()
        {
            AddNewContact(false);
        }

        [Test]
        public void AddNewContactTest()
        {
            AddNewContact(true);
        }

        private bool IsElementPresent(By by, IWebDriver driver)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private bool IsAlertPresent(IWebDriver driver)
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText(IWebDriver driver)
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}