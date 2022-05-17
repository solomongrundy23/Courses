using AddressBookAutotests.Controllers;
using JsonHelper;
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
                TestContext.AddFormatter(obj =>
                {
                    if (obj == null)
                        return x => "";

                    return x => x.ToJson(true);
                });
                if (_controllersManager == null) throw new NullReferenceException("Manager is null");
                return _controllersManager;
            }
            private set { _controllersManager = value; }
        }
        private ControllersManager _controllersManager;

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