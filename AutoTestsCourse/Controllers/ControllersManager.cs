using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Threading;

namespace AddressBookAutotests.Controllers
{
    public class ControllersManager
    {
        private static ThreadLocal<ControllersManager> _managers = new ThreadLocal<ControllersManager>();
        public static ControllersManager GetInstance()
        {
            if (!_managers.IsValueCreated)
            {
                _managers.Value = new ControllersManager();
            }
            return _managers.Value ?? throw new ArgumentNullException("Thread values == null");
        }

        public ChromeDriver Driver { get; private set; }

        private ControllersManager()
        {
            Driver = new ChromeDriver();
            Driver.Manage().Window.Maximize();
        }

        private AuthorizationController authorization;
        private ContactsController contacts;
        private GroupsControllers groups;
        private NavigateContoller navigate;
        private Scenarios scenarios;

        public AuthorizationController Authorization
        {
            get
            {
                if (authorization == null) authorization = new AuthorizationController(this);
                return authorization;
            }
        }
        public ContactsController Contacts
        {
            get
            {
                if (contacts == null) contacts = new ContactsController(this);
                return contacts;
            }
        }
        public GroupsControllers Groups
        {
            get
            {
                if (groups == null) groups = new GroupsControllers(this);
                return groups;
            }
        }
        public NavigateContoller Navigate
        {
            get
            {
                if (navigate == null) navigate = new NavigateContoller(this);
                return navigate;
            }
        }

        public Scenarios Scenarios
        {
            get
            {
                if (scenarios == null) scenarios = new Scenarios(this);
                return scenarios;
            }
        }

        ~ControllersManager()
        {
            Driver.Quit();
        }

        public ControllersManager Sleep(int seconds)
        { 
            Thread.Sleep(seconds * 1000);
            return this;
        }
    }
}