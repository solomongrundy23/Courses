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

        private ReturnedGroups IfGroupsIsEmptyCreate()
        {
            var groups = Manager.Groups.GroupList;
            if (groups.Count == 0)
            {
                AddGroup();
                groups = Manager.Groups.GroupList;
            }
            return groups;
        }

        private ReturnedContacts IfContactsIsEmptyCreate()
        {
            IfGroupsIsEmptyCreate();
            var contacts = Manager.Contacts.ContactList;
            if (contacts.Count == 0)
            {
                AddNewContact(true);
                contacts = Manager.Contacts.ContactList;
            }
            return contacts;
        }

        public void RemoveGroup()
        {
            var groups = IfGroupsIsEmptyCreate();
            var removedGroup = groups.Random().Text;

            Manager
                .Groups.SelectByName(removedGroup)
                .Groups.PressRemove();

            groups.RemoveAll(x => x.Text == removedGroup);
            groups.Sort();
            var actual = Manager.Groups.GroupList;
            actual.Sort();
            Assert.AreEqual(groups, actual);
        }

        public void EditGroup()
        {
            var group = CreateGroupData.Random();
            var groups = IfGroupsIsEmptyCreate();
            Manager
                   .Groups.SelectByName(groups.Random().Text)
                   .Groups.PressEdit()
                   .Groups.FillFields(group)
                   .Groups.PressUpdate();
        }

        public void AddNewContact(bool group, CreateContactData? contactData = null)
        {
            contactData ??= CreateContactData.Random();
            if (group)
            {
                var groups = IfGroupsIsEmptyCreate();
                contactData.New_group = groups.Random().Text;
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
            return contacts.Where(x =>
                    (x.FirstName == contactData.Firstname || contactData.Firstname == null) &&
                    (x.LastName == contactData.Lastname || contactData.Lastname == null) &&
                    (x.Address == contactData.Address || contactData.Address == null) &&
                    x.Email.OrderBy(x => x).SequenceEqual(mails) &&
                    x.Phone.OrderBy(x => x).SequenceEqual(phones)
                    ).FirstOrDefault() != null;
        }

        public void EditContact()
        { 
            EditContact(CreateContactData.Random(false));
        }

        public void EditContactFIONotChange()
        { 
            EditContact(CreateContactData.Random(true));
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
                Manager.Contacts.CheckeBoxContact(contact)
                       .Contacts.PressDelete();
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