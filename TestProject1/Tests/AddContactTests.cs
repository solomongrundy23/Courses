using AddressBookAutotests.Controllers;
using AddressBookAutotests.Models;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;

namespace AddressBookAutotests
{
    [TestFixture(typeof(FirefoxDriver))]
    [TestFixture(typeof(ChromeDriver))]
    public class AddContactTests<TypedWebDriver> where TypedWebDriver : IWebDriver, new()
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

        public void AddNewContact(bool group, Contact? contact = null)
        {
            if (manager == null) throw new NullReferenceException("ControllersManager не может быть null");
            contact ??= Contact.Random;
            if (group) contact.New_group = "Manager";
            manager
                .Navigate.NavigateHomePage()
                .Authorization.Login(Auth.Admin)
                .Navigate.AddNewContact()
                .Contacts.AddContactFillFields(contact)
                .Contacts.AddContactApply()
                .Authorization.Logout();
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
    }
}