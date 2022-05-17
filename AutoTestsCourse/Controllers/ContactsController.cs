﻿using OpenQA.Selenium;
using AddressBookAutotests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using AddressBookAutotests.Helpers;
using System.Collections.ObjectModel;

namespace AddressBookAutotests.Controllers
{
    public class ContactsController : BaseController
    {
        public ContactList ContactList
        {
            get
            {
                if (_contactList == null || _contactListChanged) 
                    Manager.Navigate.Contacts().Contacts.GetList();
                return _contactList;
            }
        }

        private ContactList _contactList;
        private bool _contactListChanged = false;
        private void ListChanged() => _contactListChanged = true;

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
            ListChanged();
            return Manager;
        }

        private readonly static string[] phonesFields = new string[]
            {
                "home",
                "mobile",
                "work",
                "phone2"
            };

        private readonly static string[] emailFields = new string[]
            {
                "email",
                "email2",
                "email3"
            };

        public Contact GetContactFromEditor(Contact contact)
        {
            PressEdit(contact);
            var result = new Contact();
            result.FirstName = GetTextBoxValue("firstname");
            result.LastName = GetTextBoxValue("lastname");
            result.Address = GetTextBoxValue("address");
            result.Phones = new List<string>();
            foreach (var phoneType in phonesFields)
            { 
                var phoneText = GetTextBoxValue(phoneType);
                if (!string.IsNullOrEmpty(phoneText)) result.Phones.Add(phoneText.DigitsOnly());
            }
            result.Emails = new List<string>();
            foreach (var emailType in emailFields)
            {
                var emailText = GetTextBoxValue(emailType);
                if (!string.IsNullOrEmpty(emailText)) result.Emails.Add(emailText);
            }
            return result;
        }

        public ControllersManager OpenList()
        {
            Driver.FindElement(By.XPath("//*[@id='nav']/ul/li[1]/a")).Click();
            return Manager;
        }

        public ReadOnlyCollection<IWebElement> GetList()
        {
            OpenList();
            var contactsRows = Driver.FindElements(By.Name("entry"));
            var tempList = new ContactList();
            foreach (var contact in contactsRows)
            {
                string lastName = GetContactLastname(contact);
                string firstName = GetContactFirstName(contact);
                string address = GetContactAddress(contact);
                List<string> email = GetEmails(contact);
                List<string> phone = GetPhones(contact);

                tempList.Add(
                    new Contact(lastName, firstName, address, email, phone)
                    );
            }
            _contactList = tempList;
            return contactsRows;
        }

        public IWebElement GetContact(Contact contact)
        {
            OpenList();
            var contactsRows = Driver.FindElements(By.Name("entry"));
            return contactsRows.Where( x => ContactEqualTable(x, contact)).FirstOrDefault() ?? throw new Exception("Contact not found");
        }

        private bool ContactEqualTable(IWebElement contactsRow, Contact contact)
        {
            if (GetContactLastname(contactsRow) == contact.LastName)
                if (GetContactFirstName(contactsRow) == contact.FirstName)
                    if (GetContactAddress(contactsRow) == contact.Address)
                        if (GetEmails(contactsRow).SequenceEqual(contact.Emails))
                            if (GetPhones(contactsRow).SequenceEqual(contact.Phones))
                                return true;
            return false;
        }

        private string GetContactLastname(IWebElement contactRow) => contactRow.FindElement(By.XPath("td[2]")).Text;
        private string GetContactFirstName(IWebElement contactRow) => contactRow.FindElement(By.XPath("td[3]")).Text;
        private string GetContactAddress(IWebElement contactRow) => contactRow.FindElement(By.XPath("td[4]")).Text;
        private IWebElement GetCheckbox(IWebElement contactRow) => contactRow.FindElement(By.Name("selected[]"));
        private IWebElement GetEditButton(IWebElement contactRow) => contactRow.FindElement(By.XPath("td[8]/a/img"));
        private IWebElement GetDetailsButton(IWebElement contactRow) => contactRow.FindElement(By.XPath("td[7]/a/img"));

        private List<string> GetEmails(IWebElement contactRow)
        { 
            return contactRow.FindElements(By.XPath("td[5]/a")).Select(x => x.Text).ToList();
        }

        private List<string> GetPhones(IWebElement contactRow)
        {
            return contactRow.FindElement(By.XPath("td[6]")).Text.Split(new string[] { Environment.NewLine}, StringSplitOptions.None).ToList();
        }

        public ControllersManager CheckeBoxClick(IWebElement contactRow)
        {
            contactRow.FindElement(By.Name("selected[]")).Click();
            return Manager;
        }

        public ControllersManager CheckeBoxContactClick(Contact contact)
        {
            GetCheckbox(GetContact(contact)).Click();
            return Manager;
        }

        public ControllersManager PressRemove()
        {
            Driver.FindElement(By.XPath("//*[@id='content']/form[2]/div[2]/input")).Click();
            try
            {
                Driver.SwitchTo().Alert().Accept();
            }
            catch { }
            ListChanged();
            return Manager;
        }

        public ControllersManager PressEdit(Contact contact)
        {
            GetEditButton(GetContact(contact)).Click();
            return Manager;
        }

        public ControllersManager PressDetails(Contact contact)
        {
            GetDetailsButton(GetContact(contact)).Click();
            return Manager;
        }

        public string GetDetailsText(Contact contactElement)
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

        private bool TryGetDetail(string textboxName, out string text)
        {
            text = GetTextBoxValue(textboxName);
            return !string.IsNullOrEmpty(text);
        }

        public string FromEditiorToDetails(Contact contact)
        {
            PressEdit(contact);
            var result = new List<string>();
            var fio = Extensions.CombineParams(" ", GetTextBoxValue("firstname"), GetTextBoxValue("middlename"), GetTextBoxValue("lastname"));
            if (!string.IsNullOrEmpty(fio)) result.Add(fio);
            if (TryGetDetail("nickname", out string nickname)) result.Add(nickname);
            if (TryGetDetail("title", out string title)) result.Add(title);
            if (TryGetDetail("company", out string company)) result.Add(company);
            if (TryGetDetail("address", out string address)) result.Add(address);
            if (result.Count != 0) result.Add("");
            if (TryGetDetail("home", out string home)) result.Add("H: " + home);
            if (TryGetDetail("mobile", out string mobile)) result.Add("M: " + mobile);
            if (TryGetDetail("work", out string work)) result.Add("W: " + work);
            if (TryGetDetail("fax", out string fax)) result.Add("F: " + fax);
            if (result.Count != 0) result.Add("");
            if (TryGetDetail("email", out string email)) result.Add(email);
            if (TryGetDetail("email2", out string email2)) result.Add(email2);
            if (TryGetDetail("email3", out string email3)) result.Add(email3);
            if (TryGetDetail("homepage", out string homepage)) result.Add("Homepage:" + homepage.Replace("https://", "\r\n").Replace("http://", "\r\n"));
            if (result.Count != 0) result.Add("");


            var birthDate = Extensions.CombineParams(" ", GetTextBoxValue("bday") + ".", GetTextBoxValue("bmonth").FirstLetterToUpperCase(), GetTextBoxValue("byear"));
            if (!string.IsNullOrEmpty(birthDate)) result.Add($"Birthday {birthDate} ({CalcFullYears(birthDate)})");

            var anniversary = Extensions.CombineParams(" ", GetTextBoxValue("aday") + ".", GetTextBoxValue("amonth").FirstLetterToUpperCase(), GetTextBoxValue("ayear"));
            if (!string.IsNullOrEmpty(anniversary)) result.Add($"Anniversary {anniversary} ({CalcAnniversaryFullYears(anniversary)})");

            if (result.Count != 0) result.Add("");
            if (TryGetDetail("address2", out string address2)) result.Add(address2);
            if (result.Count != 0) result.Add("");
            if (TryGetDetail("phone2", out string phone2)) result.Add("P: " + phone2);
            if (result.Count != 0) result.Add("");
            if (TryGetDetail("notes", out string notes)) result.Add(notes);
            return string.Join(Environment.NewLine, result);
        }

        public ControllersManager PressDeleteFromEdtior()
        {
            Driver.FindElement(By.XPath("//*[@id='content']/form[2]/input[2]")).Click();
            ListChanged();
            return Manager;
        }

        public ControllersManager PressUpdate()
        {
            Driver.FindElement(By.XPath("//*[@id='content']/form[1]/input[22]")).Click();
            ListChanged();
            return Manager;
        }
    }
}