using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using AddressBookAutotests.Models;

namespace AddressBookAutotests.Controllers
{
    public class ContactsController : BaseController
    {
        public ContactsController(ControllersManager manager) : base(manager) { }

        public ControllersManager AddContactFillFields(CreateContactData contact)
        {
            FillTextBox("firstname", contact.Firstname);
            FillTextBox("middlename", contact.Middlename);
            FillTextBox("lastname", contact.Lastname);
            FillTextBox("nickname", contact.Nickname);
            FillTextBox("title", contact.Title);
            FillTextBox("company", contact.Company);
            FillTextBox("address", contact.Address);
            FillTextBox("home", contact.Home);
            FillTextBox("mobile", contact.Mobile);
            FillTextBox("work", contact.Work);
            FillTextBox("fax", contact.Fax);
            FillTextBox("email", contact.Email);
            FillTextBox("email2", contact.Email2);
            FillTextBox("email3", contact.Email3);
            FillTextBox("homepage", contact.Homepage);

            SelectElementInComboBox("bday", contact.Bday);
            SelectElementInComboBox("bmonth", contact.Bmonth);

            FillTextBox("byear", contact.Byear);

            SelectElementInComboBox("aday", contact.Aday);
            SelectElementInComboBox("amonth", contact.Amonth);

            FillTextBox("ayear", contact.Ayear);

            SelectElementInComboBox("new_group", contact.New_group);

            FillTextBox("address2", contact.Address2);
            FillTextBox("phone2", contact.Phone2);
            FillTextBox("notes", contact.Notes);

            return Manager;
        }

        public ControllersManager PressAddContactApply()
        {
            Driver.FindElement(By.Name("submit")).Click();
            return Manager;
        }

        public ControllersManager GetContactList(out ReturnedContacts elements)
        {
            elements = new ReturnedContacts(Driver.FindElements(By.Name("entry")));
            return Manager;
        }

        public ControllersManager CheckeBoxContact(IWebElement element)
        {
            element.FindElement(By.Name("selected[]")).Click();
            return Manager;
        }

        public ControllersManager PressDelete()
        {
            Driver.FindElement(By.XPath("//*[@id='content']/form[2]/div[2]/input")).Click();
            try
            {
                Driver.SwitchTo().Alert().Accept();
            }
            catch { }
            return Manager;
        }

        public ControllersManager PressEdit(IWebElement contactElement)
        {
            contactElement.FindElement(By.XPath("//td[8]/a/img")).Click();
            return Manager;
        }

        public ControllersManager PressDeleteFromEdtior()
        {
            Driver.FindElement(By.XPath("//*[@id=\"content\"]/form[2]/input[2]")).Click();
            return Manager;
        }

        public ControllersManager PressUpdate()
        {
            Driver.FindElement(By.XPath("//*[@id='content']/form[1]/input[22]")).Click();
            return Manager;
        }
    }
}