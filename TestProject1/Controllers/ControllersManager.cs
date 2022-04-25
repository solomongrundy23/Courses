using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
            return _managers.Value ?? throw new ArgumentNullException("Thread values == null");
        }

        public IWebDriver Driver { get; private set; }

        private ControllersManager()
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnApplicationClose);
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

        private void DestroyManager()
        {
            Driver.Quit();
            Driver.Dispose();
            GC.SuppressFinalize(this);
        }

        public void OnApplicationClose(object? sender, EventArgs e)
        {
            this.Dispose();
        }

        public ControllersManager Sleep(int seconds)
        { 
            Thread.Sleep(seconds * 1000);
            return this;
        }

        public void Dispose()
        {
            DestroyManager();
        }
    }
}