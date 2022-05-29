using OpenQA.Selenium;
using AddressBookAutotests.Models;
using System.Linq;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using AddressBookAutotests.Helpers;
using NUnit.Framework;

namespace AddressBookAutotests.Controllers
{
    public class GroupsControllers : BaseController
    {

        public Groups GroupList
        {
            get
            {
                if (_groupList == null || _groupListChanded) _groupList = GetListFromDB();
                return _groupList;
            }
        }

        private Groups _groupList;
        private bool _groupListChanded;
        private void GroupsChanged() => _groupListChanded = true;

        ~GroupsControllers()
        {
            _groupList = null;
        }

        public Groups GetListFromDB()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                var dbResult = from g in db.groupDatas select g;
                Groups groups = dbResult.Select(x => new WebGroup(x.Name)).ToGroups();
                return groups;
            }
        }

        public List<GroupData> GetDataFromDB()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                var data = from g in db.groupDatas select g;
                return data.Where(x => x.Deprecated == "0000-00-00 00:00:00").ToList();
            }
        }

        public GroupsControllers(ControllersManager manager) : base(manager) { }

        public ControllersManager FillFields(GroupData group)
        {
            FillTextBox("group_name", group.Name);
            FillTextBox("group_header", group.Header);
            FillTextBox("group_footer", group.Footer);
            return Manager;
        }

        public ControllersManager PressSubmit()
        {
            Driver.FindElement(By.Name("submit")).Click();
            GroupsChanged();
            return Manager;
        }

        public ControllersManager PressUpdate()
        {
            Driver.FindElement(By.Name("update")).Click();
            GroupsChanged();
            return Manager;
        }

        public ReadOnlyCollection<IWebElement> GetList()
        {
            Manager.Navigate.Groups();
            var groups = new Groups();
            var groupElements = Driver.FindElements(By.ClassName("group"));
            foreach (var groupElement in groupElements)
            {
                groups.Add(groupElement.Text);
            }
            _groupList = groups;
            return groupElements;
        }

        public IWebElement GetGroupElement(WebGroup group)
        {
            return GetList().Where(x => x.Text == group.Name).FirstOrDefault() ?? throw new Exception("Group not found");
        }

        public ControllersManager CheckBoxClick(WebGroup group)
        {
            CheckBoxClick(GetGroupElement(group));
            return Manager;
        }

        public void CheckBoxClick(IWebElement groupElement)
        {
            groupElement.FindElement(By.TagName("input")).Click();
        }

        public bool GroupIsCreated()
        {
            try
            {
                var element = Driver.FindElement(By.ClassName("msgbox"));
                return element.Text == "A new group has been entered into the address book.\r\nreturn to the group page";
            }
            catch (NoSuchElementException ex)
            {
                return false;
            }
        }

        public ControllersManager PressRemove()
        {
            Driver.FindElement(By.Name("delete")).Click();
            GroupsChanged();
            return Manager;
        }

        public ControllersManager PressEdit()
        {
            Driver.FindElement(By.Name("edit")).Click();
            return Manager;
        }
    }
}