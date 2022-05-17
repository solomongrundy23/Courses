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

        public bool IsLoggedIn()
        {
            return ExistsElement("logout");
        }

        public bool IsLoggedIn(string userName)
        {
            try
            {
                return Manager
                    .Driver
                    .FindElement(By.XPath("//*[@id='top']/form/b"))
                    .Text == $"({userName})";
            }
            catch (NoSuchElementException ex)
            {
                return false;
            }
        }

        public ControllersManager Logout()
        {
            Manager.Driver.FindElement(By.LinkText("Logout")).Click();
            return Manager;
        }
    }
}
