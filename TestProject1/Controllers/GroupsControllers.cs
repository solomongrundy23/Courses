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
            Driver.FindElement(By.Name("group_name")).Click();
            Driver.FindElement(By.Name("group_name")).Clear();
            Driver.FindElement(By.Name("group_name")).SendKeys(group.Name);
            Driver.FindElement(By.Name("group_header")).Click();
            Driver.FindElement(By.Name("group_header")).Clear();
            Driver.FindElement(By.Name("group_header")).SendKeys(group.Header);
            Driver.FindElement(By.Name("group_footer")).Click();
            Driver.FindElement(By.Name("group_footer")).Clear();
            Driver.FindElement(By.Name("group_footer")).SendKeys(group.Footer);
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