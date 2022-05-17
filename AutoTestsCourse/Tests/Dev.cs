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
        [Description("Develop")]
        public void Test()
        {
            var cs = Manager.Contacts.GetList();
            foreach (var contact in cs) Manager.Contacts.CheckeBoxClick(contact);
            Manager.Contacts.PressRemove();

            var gs = Manager.Groups.GetList();
            for (int i = 0; i < gs.Count; i += 1) Manager.Groups.CheckBoxClick(gs[i]);
            Manager.Groups.PressRemove();
        }
    }
}
