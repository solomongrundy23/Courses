using AddressBookAutotests.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

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
                .Navigate.ToGroups()
                .Navigate.AddNewGroup()
                    .Groups.FillFields(group)
                    .Groups.PressSubmit()
                    .Groups.GroupIsCreated()
            );

            var groupsList =
            Manager
                .Navigate.ToGroups()
                .Groups.GetList()
                .Groups.CachedList;
            Assert.NotNull(groupsList.FindFirst(group.Name));
        }

        public void RemoveGroup()
        {
            var groups = Manager
                .Navigate.ToGroups()
                .Groups.GetList()
                .Groups.CachedList;
            if (groups.Count == 0)
            {
                AddGroup();
                groups = Manager
                    .Navigate.ToGroups()
                    .Groups.GetList()
                    .Groups.CachedList;
            }

            var removedGroup = groups.Random().Text;

            Manager
                .Groups.SelectByName(removedGroup)
                .Groups.PressRemove();

            groups = Manager
                        .Navigate.ToGroups()
                        .Groups.GetList()
                        .Groups.CachedList;

            groups.RemoveAll(x => x.Equals(removedGroup));
            groups.Sort();
            var actual = Manager.Groups.GetList().Groups.CachedList;
            actual.Sort();
            Assert.AreEqual(groups, actual);
        }

        public void EditGroup()
        {
            var group = CreateGroupData.Random();
            var groups = Manager
                .Navigate.ToGroups()
                .Groups.GetList()
                .Groups.CachedList;
            if (groups.Count == 0)
            {
                AddGroup(CreateGroupData.Random());
                groups = Manager
                    .Navigate.ToGroups()
                    .Groups.GetList()
                    .Groups.CachedList;
            }
            Manager
                   .Groups.SelectByName(groups.Random().Text)
                   .Groups.PressEdit()
                   .Groups.FillFields(group)
                   .Groups.PressUpdate();
        }

        public void AddNewContact(bool group, CreateContactData? contact = null)
        {
            contact ??= CreateContactData.Random();
            if (group)
            {
                var groups = Manager
                    .Navigate.ToGroups()
                    .Groups.GetList()
                    .Groups.CachedList;
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

        public void EditContact(CreateContactData contactData)
        {
            var groups = Manager
                .Navigate.ToGroups()
                .Groups.GetList()
                .Groups.CachedList;
            if (groups.Count == 0)
            {
                AddGroup(CreateGroupData.Random());
                Manager
                    .Navigate.ToGroups()
                    .Groups.GetList();
            }
            var contacts = Manager
                .Navigate.HomePage()
                .Contacts.GetList()
                .Contacts.CachedList;
            if (contacts.Count == 0)
            {
                AddNewContact(true);
                contacts = Manager
                    .Navigate.HomePage()
                    .Contacts.GetList()
                    .Contacts.CachedList;
            }
            Manager
                   .Contacts.PressEdit(contacts.Random())
                   .Contacts.AddContactFillFields(contactData)
                   .Contacts.PressUpdate();
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
            if (
                Manager
                .Navigate.HomePage()
                .Contacts.GetList()
                .Contacts.CachedList.Count() == 0
                )
            {
                AddNewContact(true);
            }
            var contacts = Manager
                .Sleep(2)
                .Navigate.HomePage()
                .Contacts.GetList()
                .Contacts.CachedList;

            var contact = contacts.Random();
            if (fromEditor)
            {
                Manager.Contacts.PressEdit(contact)
                       .Contacts.PressDeleteFromEdtior();
            }
            else
            {
                Manager.Contacts.CheckeBoxContact(contact)
                       .Contacts.PressDelete();
            }

            contacts.RemoveAll(x => x.Equals(contact));
            contacts.Sort();
            var actual = Manager.Contacts.GetList().Contacts.CachedList;
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