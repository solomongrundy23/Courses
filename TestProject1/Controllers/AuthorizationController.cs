using OpenQA.Selenium;
using AddressBookAutotests.Models;

namespace AddressBookAutotests.Controllers
{
    public class AuthorizationController : BaseController
    {
        public AuthorizationController(ControllersManager manager) : base(manager) { }

        public ControllersManager Login(Auth auth)
        {
            Manager.driver.FindElement(By.Name("user")).Click();
            Manager.driver.FindElement(By.Name("user")).Clear();
            Manager.driver.FindElement(By.Name("user")).SendKeys(auth.Username);
            Manager.driver.FindElement(By.Name("pass")).Clear();
            Manager.driver.FindElement(By.Name("pass")).SendKeys(auth.Password);
            Manager.driver.FindElement(By.XPath("//input[@value='Login']")).Click();
            return Manager;
        }

        public ControllersManager Logout()
        {
            Manager.driver.FindElement(By.LinkText("Logout")).Click();
            return Manager;
        }
    }
}
