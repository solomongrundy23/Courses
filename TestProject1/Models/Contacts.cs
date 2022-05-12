using Bogus;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using JsonHelper;
using AddressBookAutotests.Helpers;
using System.Linq;

namespace AddressBookAutotests.Models
{
    public class ReturnedContact : IEquatable<ReturnedContact>, IComparable<ReturnedContact>
    {
        public IWebElement? CheckBox { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public List<string>? Email { get; set; }
        public List<string>? Phone { get; set; }
        public string? Address { get; set; }
        public IWebElement? Edit { get; set; }
        public IWebElement? Details { get; set; }

        public ReturnedContact() { }

        public ReturnedContact(
            IWebElement checkBox, 
            string lastName, 
            string firstName, 
            string address, 
            IWebElement edit, 
            List<string> email, 
            List<string> phone, 
            IWebElement details)
        { 
            CheckBox = checkBox;
            LastName = lastName;
            FirstName = firstName;
            Address = address;
            Email = email;
            Phone = phone;
            Edit = edit;
            Details = details;
        }

        public override string ToString() => $"LastName: {LastName}\r\nFirstName: {FirstName}\r\n" +
            $"Address: {Address}\r\nPhones: {string.Join(Environment.NewLine, Phone)}\r\nEmails: {string.Join(Environment.NewLine, Email)}";

        public int CompareTo(ReturnedContact? other)
        {
            int result = string.Compare(this.LastName, other?.LastName);
            if (result == 0)
            {
                result = string.Compare(this.FirstName, other?.FirstName);
                if (result == 0) 
                    result = string.Compare(this.Address, other?.Address);
            }
            return result;
        }

        public bool Equals(ReturnedContact? other)
        {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            return other?.LastName == LastName &&
                   other?.FirstName == FirstName &&
                   other?.Address == Address &&
                   Phone.SequenceEqual(other?.Phone) &&
                   Email.SequenceEqual(other?.Email);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(LastName, FirstName, Address);
        }
    }
    public class ReturnedContacts : List<ReturnedContact>
    {
        public bool isEmpty => this.Count == 0;

        public ReturnedContact Random()
        {
            if (this.Count == 0) throw new Exception("ContactsTable is Empty");
            return this[new Random().Next(this.Count - 1)];
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
    public class CreateContactData
    {
        public string? Aday;
        public string? Address;
        public string? Address2;
        public string? Amonth;
        public string? Ayear;
        public string? Bday;
        public string? Bmonth;
        public string? Byear;
        public string? Company;
        public string? Email;
        public string? Email2;
        public string? Email3;
        public string? Fax;
        public string? Firstname;
        public string? Home;
        public string? Homepage;
        public string? Lastname;
        public string? Middlename;
        public string? Mobile;
        public string? New_group;
        public string? Nickname;
        public string? Notes;
        public string? Phone2;
        public string? Theform;
        public string? Title;
        public string? Work;

        //public override string ToString() => this.ToJson();

        public CreateContactData() { }
        public CreateContactData(
            string aday,
            string address,
            string address2,
            string amonth,
            string ayear,
            string bday,
            string bmonth,
            string byear,
            string company,
            string email,
            string email2,
            string email3,
            string fax,
            string firstname,
            string home,
            string homepage,
            string lastname,
            string middlename,
            string mobile,
            string new_group,
            string nickname,
            string notes,
            string phone2,
            string theform,
            string title,
            string work
            )
        {
            Aday = aday;
            Address = address;
            Address2 = address2;
            Amonth = amonth;
            Ayear = ayear;
            Bday = bday;
            Bmonth = bmonth;
            Byear = byear;
            Company = company;
            Email = email;
            Email2 = email2;
            Email3 = email3;
            Fax = fax;
            Firstname = firstname;
            Home = home;
            Homepage = homepage;
            Lastname = lastname;
            Middlename = middlename;
            Mobile = mobile;
            New_group = new_group;
            Nickname = nickname;
            Notes = notes;
            Phone2 = phone2;
            Theform = theform;
            Title = title;
            Work = work;
        }

        public List<string> GetPhones()
        {
            var result = new List<string>
            {
                Home.DigitsOnly(),
                Mobile.DigitsOnly(),
                Phone2.DigitsOnly(),
                Work.DigitsOnly()
            };
            result.Remove(null);
            return result;
        }

        public List<string> GetMails()
        {
            var result = new List<string>
            {
                Email,
                Email2,
                Email3
            };
            result.Remove(null);
            return result;
        }

        public static CreateContactData Random(bool fioIsNull = false)
        {
            CreateContactData result = new CreateContactData();
            Faker fakerRu = new Faker("ru");
            Faker faker = new Faker();
            result.Aday = faker.Random.Int(1, 29).ToString();
            result.Address = fakerRu.Address.FullAddress();
            result.Address2 = fakerRu.Address.FullAddress();
            result.Amonth = faker.Date.Month();
            result.Ayear = faker.Random.Int(1900, 2022).ToString();
            result.Bday = faker.Random.Int(1, 29).ToString();
            result.Bmonth = faker.Date.Month();
            result.Byear = faker.Random.Int(1900, 2022).ToString();
            result.Company = fakerRu.Company.CompanyName();
            result.Email = faker.Internet.Email();
            result.Email2 = faker.Internet.Email();
            result.Email3 = faker.Internet.Email();
            result.Fax = fakerRu.Phone.PhoneNumber();
            result.Home = fakerRu.Phone.PhoneNumber();
            result.Homepage = faker.Internet.Url();
            if (!fioIsNull)
            {
                FIO fio = FIO.Random();
                result.Firstname = fio.Name;
                result.Lastname = fio.SurName;
                result.Middlename = fio.FatherName;
            }
            result.Mobile = fakerRu.Phone.PhoneNumber();
            result.Nickname = fakerRu.Random.Word();
            result.Notes = fakerRu.Random.Words();
            result.Phone2 = fakerRu.Phone.PhoneNumber();
            result.Theform = faker.Random.Word();
            result.Title = fakerRu.Random.Word();
            result.Work = fakerRu.Phone.PhoneNumber();
            return result;
        }
    }
}