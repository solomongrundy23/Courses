using AddressBookAutotests.Models;
using NUnit.Framework;
using Bogus;

namespace AddressBookAutotests.Tests
{
    [TestFixture]
    public class AuthTests : BaseTest
    {
        [SetUp]
        public void SetUp()
        {
            try
            {
                Manager.Authorization.Logout();
            }
            catch
            { }
        }

        [Test]
        [Order(1)]
        public void Login()
        {
            Manager.Scenarios.Login(Auth.Admin);
        }

        [Test]
        [Order(2)]
        public void LoginWrongLoging()
        {
            Manager.Scenarios.Login(new Auth(Auth.Admin.Username, new Faker().Internet.Password()), false);
        }

        [Test]
        [Order(3)]
        public void LoginWrongPassword()
        {
            Manager.Scenarios.Login(new Auth(new Faker().Internet.UserName(), Auth.Admin.Username), false);
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                Manager.Authorization.Logout();
            }
            catch { }
            try
            {
                //Manager.Scenarios.Login(Auth.Admin);
            }
            catch
            { }
        }
    }
}
