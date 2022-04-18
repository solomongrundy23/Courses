using OpenQA.Selenium;
using AddressBookAutotests.Models;

namespace AddressBookAutotests.Controllers
{
    public class GroupsControllers : BaseController
    {
        public GroupsControllers(ControllersManager manager) : base(manager) { }

        public ControllersManager AddGroupFillFields(Group group)
        {
            Driver.FindElement(By.Name("group_name")).Click();
            Driver.FindElement(By.Name("group_name")).Clear();
            Driver.FindElement(By.Name("group_name")).SendKeys(group.Name);
            Driver.FindElement(By.Name("group_header")).Click();
            Driver.FindElement(By.Name("group_header")).Click();
            Driver.FindElement(By.Name("group_header")).Clear();
            Driver.FindElement(By.Name("group_header")).SendKeys(group.Header);
            Driver.FindElement(By.Name("group_footer")).Click();
            Driver.FindElement(By.Name("group_footer")).Clear();
            Driver.FindElement(By.Name("group_footer")).SendKeys(group.Footer);
            return Manager;
        }

        public ControllersManager AddGroupApply()
        {
            Driver.FindElement(By.Name("submit")).Click();
            return Manager;
        }



        public ControllersManager RemoveGroup()
        {
            Driver.FindElement(By.Name("selected[]")).Click();
            Driver.FindElement(By.XPath("//div[@id='content']/form/input[5]")).Click();
            return Manager;
        }
    }
}