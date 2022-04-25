using AddressBookAutotests.Controllers;
using NUnit.Framework;
using System;

namespace AddressBookAutotests.Tests
{
    public class BaseTest
    {
        public ControllersManager? Manager { get; set; }

        [SetUp]
        public void InitManager()
        {
            Manager = ControllersManager.GetInstance();
            Manager.Navigate.StartPage();
        }

        [TearDown]
        public void CloseManager()
        {
            try
            {

            }
            catch (Exception)
            {
            }
        }
    }
}