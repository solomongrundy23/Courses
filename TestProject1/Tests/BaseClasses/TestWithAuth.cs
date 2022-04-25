using AddressBookAutotests.Controllers;
using NUnit.Framework;
using System;

namespace AddressBookAutotests.Tests
{
    public class TestWithAuth : BaseTest
    {
        [SetUp]
        public void Auth()
        {
            Manager.Authorization.Login(Models.Auth.Admin);
        }

        [TearDown]
        public void Logout()
        {
            Manager.Authorization.Logout();
        }
    }
}