using AddressBookAutotests.Controllers;
using NUnit.Framework;
using System;

namespace AddressBookAutotests.Tests
{
    public class BaseTest
    {
        private ControllersManager? _manager;
        public ControllersManager Manager
        {
            get { if (_manager == null) throw new NullReferenceException("Manager is null"); else return _manager; }
            private set { _manager = value; }
        }

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