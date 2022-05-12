using OpenQA.Selenium;
using AddressBookAutotests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using AddressBookAutotests.Helpers;

namespace AddressBookAutotests.Controllers
{
    public class ContactsController : BaseController
    {
        public ReturnedContacts? ContactList
        {
            get
            {
                Manager.Navigate.Contacts().Contacts.OpenList();
                return _contactList;
            }
        }

        private ReturnedContacts? _contactList;

        ~ContactsController()
        {
            _contactList = null;
        }

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

        private static string[] phonesFields = new string[]
            {
                "home",
                "mobile",
                "work"
            };

        private static string[] emailFields = new string[]
            {
                "email",
                "email2",
                "email3"
            };

        public ReturnedContact GetContactFromEditor(ReturnedContact contact)
        {
            contact.Edit.Click();
            var result = new ReturnedContact();
            result.FirstName = GetTextBoxValue("firstname");
            result.LastName = GetTextBoxValue("lastname");
            result.Address = GetTextBoxValue("address");
            result.Phone = new List<string>();
            foreach (var phoneType in phonesFields)
            { 
                var phoneText = GetTextBoxValue(phoneType);
                if (!string.IsNullOrEmpty(phoneText)) result.Phone.Add(phoneText);
            }
            result.Email = new List<string>();
            foreach (var emailType in emailFields)
            {
                var emailText = GetTextBoxValue(emailType);
                if (!string.IsNullOrEmpty(emailText)) result.Phone.Add(emailText);
            }
            return result;
        }

        public ControllersManager OpenList()
        {
            Driver.FindElement(By.XPath("//*[@id='nav']/ul/li[1]/a")).Click();
            var contactsRows = Driver.FindElements(By.Name("entry"));
            var tempList = new ReturnedContacts();
            foreach (var contact in contactsRows)
            {
                IWebElement checkBox = contact.FindElement(By.Name("selected[]"));
                string lastName = contact.FindElement(By.XPath("td[2]")).Text;
                string firstName = contact.FindElement(By.XPath("td[3]")).Text;
                string address = contact.FindElement(By.XPath("td[4]")).Text;
                IWebElement details = contact.FindElement(By.XPath("td[7]/a/img"));
                IWebElement edit = contact.FindElement(By.XPath("td[8]/a/img"));
                List<string> email = GetEmails(contact);

                List<string> phone = GetPhones(contact);

                tempList.Add(
                    new ReturnedContact(checkBox, lastName, firstName, address, edit, email, phone, details)
                    );
            }
            _contactList = tempList;
            return Manager;
        }

        public ReturnedContact FindContactInContactTableWithUpdateLinks(ReturnedContact contact)
        {
            return ContactList.Find(x => x.Equals(contact));
        }

        private List<string> GetEmails(IWebElement contactRow)
        { 
            return contactRow.FindElements(By.XPath("td[5]/a")).Select(x => x.Text).ToList();
        }

        private List<string> GetPhones(IWebElement contactRow)
        {
            return contactRow.FindElement(By.XPath("td[6]")).Text.Split(new string[] { Environment.NewLine}, StringSplitOptions.None).ToList();
        }

        public ControllersManager CheckeBoxContact(ReturnedContact contactElement)
        {
            contactElement.CheckBox.Click();
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

        public ControllersManager PressEdit(ReturnedContact contactElement)
        {
            contactElement.Edit.Click();
            return Manager;
        }

        public ControllersManager PressDetails(ReturnedContact contactElement)
        {
            contactElement.Details.Click();
            return Manager;
        }

        public string GetDetailsText(ReturnedContact contactElement)
        { 
            PressDetails(contactElement);
            return Driver.FindElement(By.XPath("//*[@id='content']")).Text;
        }

        private string CalcFullYears(string birthdate)
        {
            return FullYears(DateTime.Parse(birthdate), DateTime.Now).ToString();
        }

        private string CalcAnniversaryFullYears(string birthdate)
        {
            return (DateTime.Now.Year - DateTime.Parse(birthdate).Year).ToString();
        }

        private int FullYears(DateTime dt1, DateTime dt2)
        {
            if (dt2.Year <= dt1.Year)
                return 0;
            int n = dt2.Year - dt1.Year;
            if (dt1.DayOfYear > dt2.DayOfYear)
                --n;
            return n;
        }

        public string FromEditiorToDetails(ReturnedContact contact)
        {
            PressEdit(contact);
            var result = new List<string>();
            result.Add(
                $"{GetTextBoxValue("firstname")} {GetTextBoxValue("middlename")} {GetTextBoxValue("lastname")}"
                );
            result.Add(GetTextBoxValue("nickname"));
            result.Add(GetTextBoxValue("title"));
            result.Add(GetTextBoxValue("company"));
            result.Add(GetTextBoxValue("address"));
            result.Add("");
            result.Add("H: " + GetTextBoxValue("home"));
            result.Add("M: " + GetTextBoxValue("mobile"));
            result.Add("W: " + GetTextBoxValue("work"));
            result.Add("F: " + GetTextBoxValue("fax"));
            result.Add("");
            result.Add(GetTextBoxValue("email"));
            result.Add(GetTextBoxValue("email2"));
            result.Add(GetTextBoxValue("email3"));
            result.Add("Homepage:" + GetTextBoxValue("homepage").Replace("https://", "\r\n").Replace("http://", "\r\n"));
            result.Add("");
            var birthDate = $"{GetTextBoxValue("bday")}. {GetTextBoxValue("bmonth").FirstLetterToUpperCase()} {GetTextBoxValue("byear")}";
            result.Add($"Birthday {birthDate} ({CalcFullYears(birthDate)})");
            var anniversary = $"{GetTextBoxValue("aday")}. {GetTextBoxValue("amonth").FirstLetterToUpperCase()} {GetTextBoxValue("ayear")}";
            result.Add($"Anniversary {anniversary} ({CalcAnniversaryFullYears(anniversary)})");
            result.Add("");
            result.Add(GetTextBoxValue("address2"));
            result.Add("");
            result.Add("P: " + GetTextBoxValue("phone2"));
            result.Add("");
            result.Add(GetTextBoxValue("notes"));
            return string.Join(Environment.NewLine, result);
        }

        public ControllersManager PressDeleteFromEdtior()
        {
            Driver.FindElement(By.XPath("//*[@id='content']/form[2]/input[2]")).Click();
            return Manager;
        }

        public ControllersManager PressUpdate()
        {
            Driver.FindElement(By.XPath("//*[@id='content']/form[1]/input[22]")).Click();
            return Manager;
        }
    }
}