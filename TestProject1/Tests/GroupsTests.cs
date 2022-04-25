using System;
using AddressBookAutotests.Controllers;
using AddressBookAutotests.Models;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace AddressBookAutotests.Tests
{
    [TestFixture]
    public class GroupsTest : TestWithAuth
    {
        [Test]
        [Order(1)]
        [Description("Add new group")]
        public void AddGroupTest()
        {
            Manager.Scenarios.AddGroup(CreateGroupData.Random());
        }

        [Test]
        [Order(2)]
        [Description("Edit group")]
        public void EditGroupTest()
        {
            Manager.Scenarios.EditGroup();
        }

        [Test]
        [Order(3)]
        [Description("Remove group")]
        public void RemoveGroupTest()
        {
            Manager.Scenarios.RemoveGroup();
        }
    }
}