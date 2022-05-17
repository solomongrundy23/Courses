using OpenQA.Selenium;
using System;

namespace AddressBookAutotests.Controllers
{
    public  class NavigateContoller : BaseController
    {
        public NavigateContoller(ControllersManager manager) : base(manager) { }

        public ControllersManager StartPage()
        {
            if (Driver.Url != ControllersSettings.BaseUrl + "/addressbook/")
            Driver.Navigate().GoToUrl($"{ControllersSettings.BaseUrl}/addressbook/");
            return Manager;
        }

        public ControllersManager Contacts()
        {
            Driver.FindElement(By.XPath("//*[@id='nav']/ul/li[1]/a")).Click();
            return Manager;
        }

        public ControllersManager AddNewContact()
        {
            Driver.FindElement(By.LinkText("add new")).Click();
            return Manager;
        }

        public ControllersManager Groups()
        {
            if (Driver.Url == ControllersSettings.BaseUrl + "/addressbook/group.php")
                if (ExistsElement("new") || !Manager.Groups.GroupIsCreated())
                    return Manager;
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
