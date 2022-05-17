using AddressBookAutotests.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddressBookAutotests.Controllers
{
    public class Scenarios
    {
        public Scenarios(ControllersManager manager)
        {
            Manager = manager;
        }

        private ControllersManager Manager { get; }

        public void AddGroup()
        {
            AddGroup(CreateGroupData.Random());
        }

        public void AddGroup(IEnumerable<CreateGroupData> groupList)
        { 
            foreach (var group in groupList) AddGroup(group);
        }

        public void AddGroup(CreateGroupData group)
        {
            Assert.IsTrue(
            Manager
                .Navigate.Groups()
                .Navigate.AddNewGroup()
                    .Groups.FillFields(group)
                    .Groups.PressSubmit()
                    .Groups.GroupIsCreated()
            );

            var groupsList = Manager.Groups.GroupList;
            Assert.NotNull(groupsList?.FindFirst(group.Name));
        }

        private Groups IfGroupsIsEmptyCreate()
        {
            if (Manager.Groups.GroupList.Count == 0)
            {
                AddGroup();
            }
            return Manager.Groups.GroupList;
        }

        private ContactList IfContactsIsEmptyCreate()
        {
            IfGroupsIsEmptyCreate();
            if (Manager.Contacts.ContactList.Count == 0)
            {
                AddNewContact(true);
            }
            return Manager.Contacts.ContactList;
        }

        public void RemoveGroup()
        {
            var groups = IfGroupsIsEmptyCreate();
            var removedGroup = groups.Random();

            Manager
                .Groups.CheckBoxClick(removedGroup)
                .Groups.PressRemove();

            groups.Sort();
            groups.Remove(groups.Where(x => x.Name == removedGroup.Name).First());
            var actual = Manager.Groups.GroupList;
            actual.Sort();
            Assert.AreEqual(groups, actual);
        }

        public void EditGroup()
        {
            var group = CreateGroupData.Random();
            var groups = IfGroupsIsEmptyCreate();
            Manager
                   .Groups.CheckBoxClick(groups.Random())
                   .Groups.PressEdit()
                   .Groups.FillFields(group)
                   .Groups.PressUpdate();
        }

        public void AddNewContact(bool group, CreateContactData contactData = null)
        {
            if (contactData == null) contactData = CreateContactData.Random();
            if (group)
            {
                var groups = IfGroupsIsEmptyCreate();
                contactData.New_group = groups.Random().Name;
            }
            Manager
              .Navigate.AddNewContact()
              .Contacts.AddContactFillFields(contactData)
              .Contacts.PressAddContactApply();

            Assert.IsTrue(ContactExistInList(contactData));
        }

        public void EditContact(CreateContactData contactData)
        {
            Manager
                   .Contacts.PressEdit(Manager.Contacts.ContactList.Random())
                   .Contacts.AddContactFillFields(contactData)
                   .Contacts.PressUpdate();

            Assert.IsTrue(ContactExistInList(contactData));
        }

        private bool ContactExistInList(CreateContactData contactData)
        {
            var contacts = Manager.Contacts.ContactList;
            var mails = contactData.GetMails();
            mails.Sort();
            var phones = contactData.GetPhones();
            phones.Sort();
            foreach (var x in contacts) 
                if (
                    (x.FirstName == contactData.Firstname || contactData.Firstname == null) &&
                    (x.LastName == contactData.Lastname || contactData.Lastname == null) &&
                    (x.Address == contactData.Address || contactData.Address == null) &&
                    x.Emails.OrderBy(y => y).SequenceEqual(mails) &&
                    x.Phones.OrderBy(y => y).SequenceEqual(phones))
                    return true;
            return false;
        }

        public void VerifyContactTableAndEdition()
        {
            var contactTable = IfContactsIsEmptyCreate().Random();
            var contactEdition = Manager.Contacts.GetContactFromEditor(contactTable);
            Assert.AreEqual(contactEdition, contactTable);
        }

        public void EditContact()
        {
            EditContact(CreateContactData.Random(false));
        }

        public void EditContactFIONotChange()
        { 
            EditContact(CreateContactData.Random(true));
        }

        public void Details()
        {
            var contact = IfContactsIsEmptyCreate().Random();
            var details = Manager.Contacts.GetDetailsText(contact);
            var contactDetails = Manager.Contacts.FromEditiorToDetails(contact);
            Assert.AreEqual(contactDetails, details);
        }

        public void RemoveContact(bool fromEditor)
        {
            var contacts = IfContactsIsEmptyCreate();

            var contact = contacts.Random();
            if (fromEditor)
            {
                Manager
                       .Contacts.PressEdit(contact)
                       .Contacts.PressDeleteFromEdtior();
            }
            else
            {
                Manager.Contacts.CheckeBoxContactClick(contact)
                       .Contacts.PressRemove();
            }

            contacts.RemoveAll(x => x.Equals(contact));
            contacts.Sort();
            var actual = Manager.Contacts.ContactList;
            actual.Sort();
            Assert.AreEqual(contacts, actual);
        }

        public void Login(Auth credentials, bool success = true)
        {
            Manager.Authorization.Login(credentials);
            var result = Manager.Authorization.IsLoggedIn(credentials.Username);
            if (success)
                Assert.IsTrue(result);
            else
                Assert.IsFalse(result);
        }
    }
}