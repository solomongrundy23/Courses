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
    public class ContactsTests<TypedWebDriver> where TypedWebDriver : IWebDriver, new()
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
        [Description("Add contact without group")]
        public void AddNewContactWithoutGroupTest()
        {
            Manager.Methods.AddNewContact(false);
        }

        [Test]
        [Description("Add contact with group")]
        public void AddNewContactTest()
        {
            Manager.Methods.AddNewContact(true);
        }

        [Test]
        [Description("Edit contact")]
        public void EditContactTest()
        {
            Manager.Methods.EditContact();
        }

        [Test]
        [Description("Remove contact from contacts page")]
        public void RemoveContactTestFromContacts()
        {
            Manager.Methods.RemoveContact(false);
        }

        [Test]
        [Description("Remove contact from contact's editor")]
        public void RemoveContactTestFromEditor()
        {
            Manager.Methods.RemoveContact(true);
        }
    }
}