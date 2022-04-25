using OpenQA.Selenium;
using AddressBookAutotests.Models;
using System.Linq;

namespace AddressBookAutotests.Controllers
{
    public class GroupsControllers : BaseController
    {
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

        public ControllersManager GetList(out ReturnedGroups groups)
        {
            groups = new ReturnedGroups();
            var groupElements = Driver.FindElements(By.ClassName("group"));
            foreach (var groupElement in groupElements)
            {
                var checkBox = groupElement.FindElement(By.TagName("input"));
                groups.Add(checkBox, checkBox.GetAttribute("value"), groupElement.Text);
            }
            return Manager;
        }

        public ControllersManager Select(string value)
        {
            var groupElements = Driver.FindElements(By.Name("selected[]"));
            groupElements.Where(x => x.GetAttribute("value") == value).First().Click();
            return Manager;
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