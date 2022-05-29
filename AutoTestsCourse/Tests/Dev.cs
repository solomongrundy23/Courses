using System;
using System.Collections.Generic;
using System.Linq;
using AddressBookAutotests.Models;
using AddressBookAutotests.Helpers;
using NUnit.Framework;

namespace AddressBookAutotests.Dev
{
    [TestFixture()]
    public class DevTest : Tests.TestWithAuth
    {
        [Test]
        [Description("Develop")]
        public void Test()
        {
            var FormUI = GetList(() => Manager.Contacts.ContactList);
            var FromDB = GetList(() =>
            {
                using (AddressBookDB db = new AddressBookDB())
                {
                    var dbResult = from c in db.contactDatas select c;
                    var contacts = dbResult.Select(x => new Contact(x)).ToContacts();
                    return contacts;
                }
            });
            FormUI.Sort();
            FromDB.Sort();
            Assert.AreEqual(FormUI, FromDB);
        }

        public Contacts GetList(Func<Contacts> func)
        {
            var start = DateTime.Now;
            var groups = (Contacts)func.Invoke();
            var stop = DateTime.Now;
            var remains = stop.Subtract(start).TotalMilliseconds;
            Console.WriteLine(remains);
            return groups;
        }
    }
}