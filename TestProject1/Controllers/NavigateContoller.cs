using OpenQA.Selenium;

namespace AddressBookAutotests.Controllers
{
    public  class NavigateContoller : BaseController
    {
        public NavigateContoller(ControllersManager manager) : base(manager) { }

        public ControllersManager NavigateHomePage()
        {
            Driver.Navigate().GoToUrl($"{ControllersSettings.BaseUrl}/addressbook/");
            return Manager;
        }

        public ControllersManager AddNewContact()
        {
            Driver.FindElement(By.LinkText("add new")).Click();
            return Manager;
        }

        public ControllersManager ToGroups()
        {
            Driver.FindElement(By.LinkText("groups")).Click();
            return Manager;
        }

        public ControllersManager AddNewGroup()
        {
            Driver.FindElement(By.Name("new")).Click();
            return Manager;
        }
    }
}
