using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Text;

namespace TestProject1
{
    internal class UnitTestContact
    {
        [TestFixture]
        public class UntitledTestCase
        {
            private IWebDriver? driver;
            private StringBuilder? verificationErrors;
            private bool acceptNextAlert = true;

            [SetUp]
            public void SetupTest()
            {
                driver = new FirefoxDriver();
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

            [Test]
            public void AddNewContactWithoutGroupTest()
            {
                var methods = new Methods(driver);
                methods.NavigateHomePage();
                methods.Auth(Auth.Admin);
                methods.AddContact(Contact.Random);
                methods.Logout();
            }

            [Test]
            public void AddNewContactTest()
            {
                var methods = new Methods(driver);
                methods.NavigateHomePage();
                methods.Auth(Auth.Admin);
                var contact = Contact.Random;
                contact.New_group = "Manager";
                methods.AddContact(Contact.Random);
                methods.Logout();
            }

            private bool IsElementPresent(By by)
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

            private bool IsAlertPresent()
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

            private string CloseAlertAndGetItsText()
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
}
