using OpenQA.Selenium;
using AddressBookAutotests.Models;

namespace AddressBookAutotests.Controllers
{
    public class AuthorizationController : BaseController
    {
        public AuthorizationController(ControllersManager manager) : base(manager) { }

        public ControllersManager Login(Auth auth)
        {
            Driver.FindElement(By.Name("user")).Click();
            Driver.FindElement(By.Name("user")).Clear();
            Driver.FindElement(By.Name("user")).SendKeys(auth.Username);
            Driver.FindElement(By.Name("pass")).Clear();
            Driver.FindElement(By.Name("pass")).SendKeys(auth.Password);
            Driver.FindElement(By.XPath("//*[@id=\"LoginForm\"]/input[3]")).Click();
            return Manager;
        }

        public ControllersManager Logout()
        {
            Manager.driver.FindElement(By.LinkText("Logout")).Click();
            return Manager;
        }
    }
}
