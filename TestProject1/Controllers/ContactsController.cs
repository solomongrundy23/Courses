using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using AddressBookAutotests.Models;

namespace AddressBookAutotests.Controllers
{
    public  class ContactsController : BaseController
    {
        public ContactsController(ControllersManager manager) : base(manager) { }

        public ControllersManager AddContactFillFields(Contact contact)
        {
            Driver.FindElement(By.Name("firstname")).Click();
            Driver.FindElement(By.Name("firstname")).Clear();
            Driver.FindElement(By.Name("firstname")).SendKeys(contact.Firstname);
            Driver.FindElement(By.Name("middlename")).Click();
            Driver.FindElement(By.Name("middlename")).Clear();
            Driver.FindElement(By.Name("middlename")).SendKeys(contact.Middlename);
            Driver.FindElement(By.Name("lastname")).Click();
            Driver.FindElement(By.Name("lastname")).Clear();
            Driver.FindElement(By.Name("lastname")).SendKeys(contact.Lastname);
            Driver.FindElement(By.Name("nickname")).Click();
            Driver.FindElement(By.Name("nickname")).Clear();
            Driver.FindElement(By.Name("nickname")).SendKeys(contact.Nickname);
            Driver.FindElement(By.Name("title")).Click();
            Driver.FindElement(By.Name("title")).Clear();
            Driver.FindElement(By.Name("title")).SendKeys(contact.Title);
            Driver.FindElement(By.Name("company")).Click();
            Driver.FindElement(By.Name("company")).Clear();
            Driver.FindElement(By.Name("company")).SendKeys(contact.Company);
            Driver.FindElement(By.Name("address")).Click();
            Driver.FindElement(By.Name("address")).Clear();
            Driver.FindElement(By.Name("address")).SendKeys(contact.Address);
            Driver.FindElement(By.Name("home")).Click();
            Driver.FindElement(By.Name("home")).Clear();
            Driver.FindElement(By.Name("home")).SendKeys(contact.Home);
            Driver.FindElement(By.Name("mobile")).Click();
            Driver.FindElement(By.Name("mobile")).Clear();
            Driver.FindElement(By.Name("mobile")).SendKeys(contact.Mobile);
            Driver.FindElement(By.XPath("//div[@id='content']/form/label[13]")).Click();
            Driver.FindElement(By.Name("work")).Click();
            Driver.FindElement(By.Name("work")).Clear();
            Driver.FindElement(By.Name("work")).SendKeys(contact.Work);
            Driver.FindElement(By.Name("fax")).Click();
            Driver.FindElement(By.Name("fax")).Clear();
            Driver.FindElement(By.Name("fax")).SendKeys(contact.Fax);
            Driver.FindElement(By.Name("email")).Click();
            Driver.FindElement(By.Name("email")).Clear();
            Driver.FindElement(By.Name("email")).SendKeys(contact.Email);
            Driver.FindElement(By.Name("email2")).Click();
            Driver.FindElement(By.Name("email2")).Clear();
            Driver.FindElement(By.Name("email2")).SendKeys(contact.Email2);
            Driver.FindElement(By.Name("email3")).Click();
            Driver.FindElement(By.Name("email3")).Clear();
            Driver.FindElement(By.Name("email3")).SendKeys(contact.Email3);
            Driver.FindElement(By.Name("homepage")).Click();
            Driver.FindElement(By.Name("homepage")).Clear();
            Driver.FindElement(By.Name("homepage")).SendKeys(contact.Homepage);
            Driver.FindElement(By.Name("bday")).Click();
            Driver.FindElement(By.Name("bday")).Click();
            new SelectElement(Driver.FindElement(By.Name("bday"))).SelectByText(contact.Bday);
            Driver.FindElement(By.Name("bmonth")).Click();
            new SelectElement(Driver.FindElement(By.Name("bmonth"))).SelectByText(contact.Bmonth);
            Driver.FindElement(By.Name("byear")).Click();
            Driver.FindElement(By.Name("byear")).Clear();
            Driver.FindElement(By.Name("byear")).SendKeys(contact.Byear);
            Driver.FindElement(By.Name("aday")).Click();
            new SelectElement(Driver.FindElement(By.Name("aday"))).SelectByText(contact.Aday);
            Driver.FindElement(By.Name("amonth")).Click();
            new SelectElement(Driver.FindElement(By.Name("amonth"))).SelectByText(contact.Amonth);
            Driver.FindElement(By.Name("ayear")).Click();
            Driver.FindElement(By.Name("ayear")).Clear();
            Driver.FindElement(By.Name("ayear")).SendKeys(contact.Ayear);
            if (contact.New_group != null)
            {
                Driver.FindElement(By.Name("new_group")).Click();
                new SelectElement(Driver.FindElement(By.Name("new_group"))).SelectByText(contact.New_group);
            }
            Driver.FindElement(By.Name("address2")).Click();
            Driver.FindElement(By.Name("address2")).Clear();
            Driver.FindElement(By.Name("address2")).SendKeys(contact.Address2);
            Driver.FindElement(By.Name("theform")).Click();
            Driver.FindElement(By.Name("phone2")).Click();
            Driver.FindElement(By.Name("phone2")).Clear();
            Driver.FindElement(By.Name("phone2")).SendKeys(contact.Phone2);
            Driver.FindElement(By.Name("notes")).Click();
            Driver.FindElement(By.Name("notes")).Clear();
            Driver.FindElement(By.Name("notes")).SendKeys(contact.Notes);
            
            return Manager;
        }

        public ControllersManager AddContactApply()
        {
            Driver.FindElement(By.XPath("//div[@id='content']/form/input[21]")).Click();
            return Manager;
        }
    }
}
