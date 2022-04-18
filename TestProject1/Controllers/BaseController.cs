using OpenQA.Selenium;

namespace AddressBookAutotests.Controllers
{
    public class BaseController
    {
        protected ControllersManager Manager { get; private set; }

        public BaseController(ControllersManager manager)
        {
            Manager = manager;
        }

        protected IWebDriver Driver { get => Manager.driver; }
    }
}
