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
        [Description("Select Contact to Group")]
        public void ContactToGroup()
        {
            Manager.Scenarios.AddContactToGroup();
        }

        [Test]
        [Order(1)]
        [Description("Unselect Contact From Group")]
        public void ContactFromGroup()
        {
            Manager.Scenarios.DeleteContactFromGroup();
        }
    }
}