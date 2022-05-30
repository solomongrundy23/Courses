using AddressBookAutotests.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using AddressBookAutotests.Helpers;

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
            AddGroup(GroupData.Random());
        }

        public void AddGroup(IEnumerable<GroupData> groupList)
        { 
            foreach (var group in groupList) AddGroup(group);
        }

        public void AddGroup(GroupData group)
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

        private Contacts IfContactsIsEmptyCreate()
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
            var group = GroupData.Random();
            var groups = IfGroupsIsEmptyCreate();
            Manager
                   .Groups.CheckBoxClick(groups.Random())
                   .Groups.PressEdit()
                   .Groups.FillFields(group)
                   .Groups.PressUpdate();
        }

        public void AddNewContact(bool group, ContactData contactData = null)
        {
            if (contactData == null) contactData = ContactData.Random();
            if (group)
            {
                var groups = IfGroupsIsEmptyCreate();
                contactData.New_group = groups.Random().Name;
            }
            var excepted = new Contacts(Manager.Contacts.ContactList);
            excepted.Add(new Contact(contactData));
            excepted.Sort();
            Manager
              .Navigate.AddNewContact()
              .Contacts.AddContactFillFields(contactData)
              .Contacts.PressAddContactApply();

            Assert.AreEqual(Manager.Contacts.ContactList, excepted);
        }

        public void EditContact(ContactData contactData)
        {
            var editContact = Manager.Contacts.ContactList.Random();
            var excepted = new Contacts(Manager.Contacts.ContactList);
            Manager
                   .Contacts.PressEdit(editContact)
                   .Contacts.AddContactFillFields(contactData)
                   .Contacts.PressUpdate();

            Assert.AreNotEqual(Manager.Contacts.ContactList, excepted);
        }

        public void AddContactToGroup()
        {
            IfGroupsIsEmptyCreate();
            IfContactsIsEmptyCreate();
            var group = Manager.Groups.GetDataFromDB().ToList().Random();
            var contact = Manager.Contacts.GetDataFromDB().ToList().Random();
            Manager.Contacts.OpenList()
                        .Contacts.SelectGroupFilter()
                        .Contacts.FindContactByIdAndClick(contact.Id.ToString())
                        .Contacts.SelectGroupToAddContact(group.Name)
                        .Contacts.PressAddToGroup()
                        .Contacts.AddedToGroupMessage(group.Name);
            var links = Manager.Contacts.GetLinksFromDB().ToList();
            Assert.NotNull(
                links.Where(x => x.GroupId == group.Id && x.ContactId == contact.Id).FirstOrDefault()
                );
        }

        public void DeleteContactFromGroup()
        {
            AddContactToGroup();
            var link = Manager.Contacts.GetLinksFromDB().ToList().Random();
            var group = Manager.Groups.GetDataFromDB().Where(x => x.Id == link.GroupId).FirstOrDefault();
            var contact = Manager.Contacts.GetDataFromDB().Where(x => x.Id == link.ContactId).FirstOrDefault();
            Manager.Contacts.OpenList()
                        .Contacts.SelectGroupFilter(group.Name)
                        .Contacts.FindContactByIdAndClick(contact.Id.ToString())
                        .Contacts.PressRemove()
                        .Contacts.ContactRemovedFromGroup(group.Name);
            var links = Manager.Contacts.GetLinksFromDB().ToList();
            Assert.Null(
                links.Where(x => x.GroupId == group.Id && x.ContactId == contact.Id).FirstOrDefault()
                );
        }

        private bool ContactExistInList(ContactData contactData)
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
                    x.Emails.SequenceEqual(mails) &&
                    x.Phones.SequenceEqual(phones))
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
            EditContact(ContactData.Random(false));
        }

        public void EditContactFIONotChange()
        { 
            EditContact(ContactData.Random(true));
        }

        public void EditContactGroupSelect()
        {
            EditContact(ContactData.Random(true));
        }

        public void Details()
        {
            var contact = IfContactsIsEmptyCreate().Random();
            var details = Manager.Contacts.GetDetailsText(contact);
            var contactDetails = Manager.Contacts.FromEditiorToDetails(contact);
            Assert.AreEqual(details, contactDetails);
        }

        public void RemoveContact(bool fromEditor)
        {
            var contacts = new Contacts(IfContactsIsEmptyCreate());

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
                       .Contacts.PressRemoveContact();
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