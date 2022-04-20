using OpenQA.Selenium;
using System;

namespace AddressBookAutotests.Controllers
{
    public class ControllersManager : IDisposable
    {
        public IWebDriver driver { get; private set; }

        public ControllersManager(IWebDriver driver)
        {
            this.driver = driver;
            driver.Manage().Window.Maximize();
        }

        private AuthorizationController? authorization;
        private ContactsController? contacts;
        private GroupsControllers? groups;
        private NavigateContoller? navigate;
        private Scenarios? methods;

        public AuthorizationController Authorization { get => authorization ??= new AuthorizationController(this); }
        public ContactsController Contacts { get => contacts ??= new ContactsController(this); }
        public GroupsControllers Groups { get => groups ??= new GroupsControllers(this); }
        public NavigateContoller Navigate { get => navigate ??= new NavigateContoller(this); }

        public Scenarios Methods { get => methods ??= new Scenarios(this); }

        public void Dispose()
        {
            try
            {
                driver.Quit();
            }
            catch { }
        }
    }
}