using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookAutotests.Tests
{
    [TestFixture]
    public class ContactGroupTest : TestWithAuth
    {
        [Test]
        [Order(1)]
        [Description("Contact to Group")]
        public void ContactToGroup()
        {
            Manager.Scenarios.AddContactToGroup();
        }
    }
}