using AddressBookAutotests.Models;
using NUnit.Framework;
using System;

namespace AddressBookAutotests.Controllers
{
    public class Scenarios
    {
        public Scenarios(ControllersManager manager)
        {
            Manager = manager;
        }

        private ControllersManager Manager { get; }

        public void AddGroup(CreateGroupData? group = null)
        {
            group ??= CreateGroupData.Random();
            Assert.IsTrue(
            Manager
                .Navigate.ToGroups()
                .Navigate.AddNewGroup()
                .Groups.FillFields(group)
                .Groups.PressSubmit()
                .Groups.GroupIsCreated()
            );
        }

        public void RemoveGroup()
        {
            Manager.Navigate.ToGroups().Groups.GetList(out var groups);
            if (groups.Count == 0)
            {
                AddGroup();
                Manager.Navigate.ToGroups().Groups.GetList(out groups);
            }

            Manager
                .Groups.Select(groups.Random().Value)
                .Groups.PressRemove();
        }

        public void EditGroup()
        {
            var group = CreateGroupData.Random();
            Manager.Navigate.ToGroups().Groups.GetList(out var groups);
            if (groups.Count == 0)
            {
                AddGroup(CreateGroupData.Random());
                Manager.Navigate.ToGroups().Groups.GetList(out groups);
            }
            Manager
                   .Groups.Select(groups.Random().Value)
                   .Groups.PressEdit()
                   .Groups.FillFields(group)
                   .Groups.PressUpdate();
        }

        public void AddNewContact(bool group, CreateContactData? contact = null)
        {
            contact ??= CreateContactData.Random();
            if (group)
            {
                Manager.Navigate.ToGroups()
                .Groups.GetList(out var groups);
                if (groups.Count == 0)
                {
                    var newGroup = CreateGroupData.Random();
                    AddGroup(newGroup);
                    contact.New_group = newGroup.Name;
                }
                else
                {
                    contact.New_group = groups.Random().Text;
                }
            }
            Manager
              .Navigate.AddNewContact()
              .Contacts.AddContactFillFields(contact)
              .Contacts.PressAddContactApply();
        }

        public void EditContact(CreateContactData? contactData = null)
        {
            Manager.Navigate.ToGroups().Groups.GetList(out var groups);
            if (groups.Count == 0)
            {
                AddGroup(CreateGroupData.Random());
                Manager.Navigate.ToGroups().Groups.GetList(out groups);
            }
            Manager.Navigate.HomePage().Contacts.GetContactList(out var contacts);
            if (contacts.ContactsTable.Count == 0)
            {
                AddNewContact(true);
                Manager.Navigate.HomePage().Contacts.GetContactList(out contacts);
            }
            Manager
                   .Contacts.PressEdit(contacts.Random())
                   .Contacts.AddContactFillFields(contactData ?? CreateContactData.Random())
                   .Contacts.PressUpdate();
        }

        public void EditContactFIONotChange()
        { 
            var contact = CreateContactData.Random();
            contact.Firstname = null;
            contact.Lastname = null;
            contact.Middlename = null;
            EditContact(contact);
        }

        public void RemoveContact(bool fromEditor)
        {
            Manager.Navigate.HomePage().Contacts.GetContactList(out var contacts);
            if (contacts.ContactsTable.Count == 0)
            {
                AddNewContact(true);
                Manager.Sleep(2).Navigate.HomePage().Contacts.GetContactList(out contacts);
            }
            if (fromEditor)
            {
                Manager.Contacts.PressEdit(contacts.Random())
                       .Contacts.PressDeleteFromEdtior();
            }
            else
            {
                Manager.Contacts.CheckeBoxContact(contacts.Random())
                       .Contacts.PressDelete();
            }
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