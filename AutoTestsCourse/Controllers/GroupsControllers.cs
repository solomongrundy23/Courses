using OpenQA.Selenium;
using AddressBookAutotests.Models;
using System.Linq;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace AddressBookAutotests.Controllers
{
    public class GroupsControllers : BaseController
    {

        public Groups GroupList
        {
            get
            {
                if (_groupList == null || _groupListChanded) Manager.Navigate.Groups().Groups.GetList();
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

        public GroupsControllers(ControllersManager manager) : base(manager) { }

        public ControllersManager FillFields(CreateGroupData group)
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

        public IWebElement GetGroupElement(Group group)
        {
            return GetList().Where(x => x.Text == group.Name).FirstOrDefault() ?? throw new Exception("Group not found");
        }

        public ControllersManager CheckBoxClick(Group group)
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