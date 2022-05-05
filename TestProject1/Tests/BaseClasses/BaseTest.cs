using AddressBookAutotests.Controllers;
using NUnit.Framework;
using System;

namespace AddressBookAutotests.Tests
{
    public class BaseTest
    {
        public ControllersManager Manager 
        {
            get
            {
                if (_controllersManager == null) throw new NullReferenceException("Manager is null");
                return _controllersManager;
            }
            private set { _controllersManager = value; }
        }
        private ControllersManager? _controllersManager;

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