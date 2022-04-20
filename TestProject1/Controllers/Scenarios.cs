using AddressBookAutotests.Models;
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

        public void AddGroup(CreateGroupData group)
        {
            Manager.Navigate.NavigateHomePage()
                .Authorization.Login(Auth.Admin)
                .Navigate.ToGroups()
                .Navigate.AddNewGroup()
                .Groups.FillFields(group)
                .Groups.PressSubmit()
                .Authorization.Logout();
        }

        public void RemoveGroup()
        {
            Manager.Navigate.NavigateHomePage()
                .Authorization.Login(Auth.Admin)
                .Navigate.ToGroups()
                .Groups.GetList(out var groups)
                .Groups.Select(groups.Random().Value)
                .Groups.PressRemove()
                .Authorization.Logout();
        }

        public void EditGroup()
        {
            var group = CreateGroupData.Random();
            Manager.Navigate.NavigateHomePage()
                .Authorization.Login(Auth.Admin)
                .Navigate.ToGroups()
                .Groups.GetList(out var groups)
                .Groups.Select(groups.Random().Value)
                .Groups.PressEdit()
                .Groups.FillFields(group)
                .Groups.PressUpdate()
                .Authorization.Logout();
        }

        public void AddNewContact(bool group, CreateContactData? contact = null)
        {
            contact ??= CreateContactData.Random();
            Manager.Navigate.NavigateHomePage()
                   .Authorization.Login(Auth.Admin);
            if (group)
            {
                Manager.Navigate.ToGroups()
                .Groups.GetList(out var groups);
                if (groups.Count == 0)
                {
                    var newGroup = CreateGroupData.Random();
                    Manager.Navigate.AddNewGroup()
                           .Groups.FillFields(newGroup)
                           .Groups.PressSubmit();
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
              .Contacts.PressAddContactApply()
              .Authorization.Logout();
        }

        public void EditContact()
        {
            Manager.Navigate.NavigateHomePage()
                   .Authorization.Login(Auth.Admin)
                    .Contacts.GetContactList(out var contacts)
                    .Contacts.PressEdit(contacts.Random())
                    .Contacts.AddContactFillFields(CreateContactData.Random())
                    .Contacts.PressUpdate()
                    .Authorization.Logout();
        }

        public void RemoveContact(bool fromEditor)
        {
                Manager.Navigate.NavigateHomePage()
                       .Authorization.Login(Auth.Admin)
                       .Contacts.GetContactList(out var contacts);
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
                Manager.Authorization.Logout();
        
        }
    }
}