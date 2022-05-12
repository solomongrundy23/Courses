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
    public class DevTest : AddressBookAutotests.Tests.TestWithAuth
    {
        [Test]
        [Description("")]
        public void Test()
        {
            var cs = Manager.Contacts.ContactList;
            foreach (var contact in cs) contact.CheckBox.Click();
            Manager.Contacts.PressDelete();
        }
    }
}
