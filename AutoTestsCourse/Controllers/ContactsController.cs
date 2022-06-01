using OpenQA.Selenium;
using AddressBookAutotests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using AddressBookAutotests.Helpers;
using System.Collections.ObjectModel;
using NUnit.Framework;
using System.Text;

namespace AddressBookAutotests.Controllers
{
    public class ContactsController : BaseController
    {
        public Contacts ContactList
        {
            get
            {
                if (_contactList == null || _contactListChanged)
                    _contactList = Manager.Navigate.Contacts().Contacts.GetListFromDB();
                return _contactList;
            }
        }

        private Contacts _contactList;
        private bool _contactListChanged = false;
        private void ListChanged() => _contactListChanged = true;

        ~ContactsController()
        {
            _contactList = null;
        }

        public ContactsController(ControllersManager manager) : base(manager) { }

        public ControllersManager AddContactFillFields(ContactData contact)
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

        public List<GroupContactLink> GetLinksFromDB()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                var data = from c in db.groupContantLinks select c;
                return data.Where(x => x.Deprecated == "0000-00-00 00:00:00").ToList();
            }
        }

        public List<ContactData> GetDataFromDB()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                var data = from c in db.contactDatas select c;
                return data.Where(x => x.Deprecated == "0000-00-00 00:00:00").ToList();
            }
        }

        public Contacts GetListFromDB()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                var contacts = GetDataFromDB().Select(x => new Contact(x)).ToContacts();
                contacts.Sort();
                return contacts;
            }
        }

        public ControllersManager SelectGroupFilter(string groupName = "[all]")
        {
            SelectElementInComboBox("group", groupName);
            return Manager;
        }

        public IWebElement FindContactById(string contactId)
        {
            return Driver.FindElement(By.Id(contactId));
        }

        public ControllersManager FindContactByIdAndClick(string contactId)
        {
            FindContactById(contactId).Click();
            return Manager;
        }

        public ControllersManager SelectGroupToAddContact(string groupName)
        {
            SelectElementInComboBox("to_group", groupName);
            return Manager;
        }

        public ControllersManager PressAddToGroup()
        {
            Driver.FindElement(By.Name("add")).Click();
            return Manager;
        }

        public ControllersManager AddedToGroupMessage(string groupNameAssert)
        {
            var msg = Driver.FindElement(By.ClassName("msgbox"));
            Assert.IsTrue(msg.Text == $"Users added.\r\nGo to group page \"{groupNameAssert}\".");
            return Manager;
        }

        public ReadOnlyCollection<IWebElement> GetList()
        {
            OpenList();
            var contactsRows = Driver.FindElements(By.Name("entry"));
            var tempList = new Contacts();
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
            return contactRow.FindElements(By.XPath("td[5]/a")).Select(x => x.Text).Where(x => x != string.Empty).ToList();
        }

        private List<string> GetPhones(IWebElement contactRow)
        {
            return contactRow.FindElement(By.XPath("td[6]")).Text.Split(new string[] { Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries).ToList();
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

        public ControllersManager PressRemoveContact()
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
        public ControllersManager PressRemove()
        {
            Driver.FindElement(By.Name("remove")).Click();
            ListChanged();
            return Manager;
        }

        public ControllersManager ContactRemovedFromGroup(string groupName)
        {
            string msgText = Driver.FindElement(By.ClassName("msgbox")).Text;
            Assert.AreEqual(msgText, $"Users removed.\r\nreturn to group page \"{groupName}\".");
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

        private string CalcFullYears(DateTime birthdate)
        {
            return $" ({FullYears(birthdate, DateTime.Now)})";
        }

        private string CalcFullYears(string birthdate)
        {
            var date = GetDateFromString(birthdate);
            if (date == null) return string.Empty;
            return CalcFullYears(date.Value);
        }

        private string CalcAnniversaryFullYears(DateTime birthdate)
        {
            return (DateTime.Now.Year - birthdate.Year).ToString();
        }

        private DateTime? GetDateFromString(string date)
        {
            if (DateTime.TryParse(date, out var dateObject))
            {
                return dateObject;
            }
            else
            { 
                return null;
            }
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
            var mainInfo = new List<string>();
            var fio = Extensions.CombineParams(" ", GetTextBoxValue("firstname"), GetTextBoxValue("middlename"), GetTextBoxValue("lastname"));
            if (!string.IsNullOrEmpty(fio)) mainInfo.Add(fio);
            if (TryGetDetail("nickname", out string nickname)) mainInfo.Add(nickname);
            if (TryGetDetail("title", out string title)) mainInfo.Add(title);
            if (TryGetDetail("company", out string company)) mainInfo.Add(company);
            if (TryGetDetail("address", out string address)) mainInfo.Add(address);

            var phonesInfo = new List<string>();
            if (TryGetDetail("home", out string home)) phonesInfo.Add("H: " + home);
            if (TryGetDetail("mobile", out string mobile)) phonesInfo.Add("M: " + mobile);
            if (TryGetDetail("work", out string work)) phonesInfo.Add("W: " + work);
            if (TryGetDetail("fax", out string fax)) phonesInfo.Add("F: " + fax);

            var emailsInfo = new List<string>();
            if (TryGetDetail("email", out string email)) emailsInfo.Add(email);
            if (TryGetDetail("email2", out string email2)) emailsInfo.Add(email2);
            if (TryGetDetail("email3", out string email3)) emailsInfo.Add(email3);
            if (TryGetDetail("homepage", out string homepage)) emailsInfo.Add("Homepage:" + homepage.Replace("https://", "\r\n").Replace("http://", "\r\n"));

            var datesInfo = new List<string>();
            var bday = GetTextBoxValue("bday");
            if (bday == "" || bday == "0") bday = ""; else bday += ".";
            var bmonth = GetTextBoxValue("bmonth").FirstLetterToUpperCase();
            var byear = GetTextBoxValue("byear");
            if (bmonth == "" || bmonth == "-") bmonth = "";
            var birthDate = Extensions.CombineParams(" ", bday, bmonth, byear);
            if (!string.IsNullOrEmpty(birthDate))
            {
                var birthDateObject = GetDateFromString(birthDate);
                datesInfo.Add($"Birthday {birthDate}{CalcFullYears(birthDate)}");
            }
            var aday = GetTextBoxValue("aday");
            if (string.IsNullOrEmpty(aday) || aday == "0") aday = ""; else aday += ".";
            var amonth = GetTextBoxValue("amonth").FirstLetterToUpperCase();
            if (amonth == "" || amonth == "-") amonth = "";
            var ayear = GetTextBoxValue("ayear");
            var anniversaryDate = Extensions.CombineParams(" ", aday, amonth, ayear);
            if (!string.IsNullOrEmpty(anniversaryDate))
            {
                var anniversaryDateObject = GetDateFromString(anniversaryDate);
                datesInfo.Add($"Anniversary {anniversaryDate}{CalcFullYears(anniversaryDate)}");
            }

            TryGetDetail("address2", out string address2);
            TryGetDetail("phone2", out string phone2);
            TryGetDetail("notes", out string notes);
            var result = new List<string>();
            if (mainInfo.Count > 0) result.Add(mainInfo.ToText());
            if (phonesInfo.Count > 0) { if (result.Count > 0) result.Add(""); result.Add(phonesInfo.ToText()); }
            if (emailsInfo.Count > 0) { if (result.Count > 0) result.Add(""); result.Add(emailsInfo.ToText()); }
            if (datesInfo.Count > 0) { if (result.Count > 0) result.Add(""); result.Add(datesInfo.ToText()); }
            if (!string.IsNullOrEmpty(address2)) { if (result.Count > 0) result.Add(""); result.Add(address2); }
            if (!string.IsNullOrEmpty(phone2)) { if (result.Count > 0) result.Add(""); result.Add($"P: {phone2}"); }
            if (!string.IsNullOrEmpty(notes)) { if (result.Count > 0) result.Add(""); result.Add(notes); }
            return result.ToText();
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