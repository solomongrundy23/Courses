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
        public ReturnedGroups CachedList { get; private set; } = new ReturnedGroups();
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
            return Manager;
        }

        public ControllersManager PressUpdate()
        {
            Driver.FindElement(By.Name("update")).Click();
            return Manager;
        }

        public ControllersManager GetList()
        {
            var groups = new ReturnedGroups();
            var groupElements = Driver.FindElements(By.ClassName("group"));
            foreach (var groupElement in groupElements)
            {
                var checkBox = groupElement.FindElement(By.TagName("input"));
                groups.Add(checkBox, checkBox.GetAttribute("value"), groupElement.Text);
            }
            CachedList = groups;
            return Manager;
        }

        public ControllersManager SelectByValue(string value)
        {
            var elements = GetGroupsElements().Where(x => x.GetAttribute("value") == value);
            CollectionClicker(elements);
            return Manager;
        }

        public ControllersManager SelectByName(string name)
        {
            var elements = GetGroupsElements().Where(x => x.GetAttribute("text") == name);
            CollectionClicker(elements);
            return Manager;
        }

        private void CollectionClicker(ICollection<IWebElement> elements)
        {
            CollectionClicker(elements.Select(x => x));
        }

        private void CollectionClicker(IEnumerable<IWebElement> elements)
        {
            foreach (var element in elements) element.Click();
        }

        private ReadOnlyCollection<IWebElement> GetGroupsElements()
        {
            return Driver.FindElements(By.Name("selected[]"));
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
            return Manager;
        }

        public ControllersManager PressEdit()
        {
            Driver.FindElement(By.Name("edit")).Click();
            return Manager;
        }
    }
}