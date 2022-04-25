using NUnit.Framework;

namespace AddressBookAutotests.Tests
{
    [TestFixture]
    public class ContactsTests : TestWithAuth
    {
        [Test]
        [Order(1)]
        [Description("Add contact without group")]
        public void AddNewContactWithoutGroupTest()
        {
            Manager.Scenarios.AddNewContact(false);
        }

        [Test]
        [Order(2)]
        [Description("Add contact with group")]
        public void AddNewContactTest()
        {
            Manager.Scenarios.AddNewContact(true);
        }

        [Test]
        [Order(3)]
        [Description("Edit contact")]
        public void EditContactTest()
        {
            Manager.Scenarios.EditContact();
        }

        [Test]
        [Order(3)]
        [Description("Edit contact")]
        public void EditContactFIONotChange()
        {
            Manager.Scenarios.EditContactFIONotChange();
        }

        [Test]
        [Order(4)]
        [Description("Remove contact from contacts page")]
        public void RemoveContactTestFromContacts()
        {
            Manager.Scenarios.RemoveContact(false);
        }

        [Test]
        [Order(5)]
        [Description("Remove contact from contact's editor")]
        public void RemoveContactTestFromEditor()
        {
            Manager.Scenarios.RemoveContact(true);
        }
    }
}