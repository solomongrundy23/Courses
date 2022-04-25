using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Threading;

namespace AddressBookAutotests.Controllers
{

    public class ControllersManager : IDisposable
    {
        private static ThreadLocal<ControllersManager> _managers = new ThreadLocal<ControllersManager>();
        public static ControllersManager GetInstance()
        {
            if (!_managers.IsValueCreated)
            {
                _managers.Value = new ControllersManager();
            }
            if (_managers.Value == null) throw new ArgumentNullException("Thread values == null");
            return _managers.Value;
        }

        public IWebDriver Driver { get; private set; }

        private ControllersManager()
        {
            Driver = new ChromeDriver();
            Driver.Manage().Window.Maximize();
        }

        private AuthorizationController? authorization;
        private ContactsController? contacts;
        private GroupsControllers? groups;
        private NavigateContoller? navigate;
        private Scenarios? scenarios;

        public AuthorizationController Authorization { get => authorization ??= new AuthorizationController(this); }
        public ContactsController Contacts { get => contacts ??= new ContactsController(this); }
        public GroupsControllers Groups { get => groups ??= new GroupsControllers(this); }
        public NavigateContoller Navigate { get => navigate ??= new NavigateContoller(this); }

        public Scenarios Scenarios { get => scenarios ??= new Scenarios(this); }

        ~ControllersManager()
        {
            DestroyManager();
        }

        public void Dispose()
        {
            DestroyManager();
        }

        private void DestroyManager()
        {
            try
            {
                Driver.Quit();
            }
            catch { }
        }

        public ControllersManager Sleep(int seconds)
        { 
            Thread.Sleep(seconds * 1000);
            return this;
        }
    }
}