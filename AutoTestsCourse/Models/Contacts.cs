using Bogus;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using JsonHelper;
using AddressBookAutotests.Helpers;
using System.Linq;
using LinqToDB.Mapping;

namespace AddressBookAutotests.Models
{
    public class Contact : IEquatable<Contact>, IComparable<Contact>
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public List<string> Emails { get; set; }
        public List<string> Phones { get; set; }
        public string Address { get; set; }

        public Contact() { }

        public Contact(
            string lastName, 
            string firstName, 
            string address, 
            List<string> email, 
            List<string> phone)
        { 
            LastName = lastName;
            FirstName = firstName;
            Address = address;
            Emails = email;
            Phones = phone;
        }

        public Contact(ContactData createData)
        {
            FirstName = createData.Firstname;
            LastName = createData.Lastname;
            Address = createData.Address;
            Emails = createData.GetMails();
            Phones = createData.GetPhones();
        }

        public override string ToString() => $"LastName: {LastName}\r\nFirstName: {FirstName}\r\n" +
            $"Address: {Address}\r\nPhones: {string.Join(Environment.NewLine, Phones)}\r\nEmails: {string.Join(Environment.NewLine, Emails)}";

        public int CompareTo(Contact other)
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

        public bool Equals(Contact other)
        {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            return other?.LastName == LastName &&
                   other?.FirstName == FirstName &&
                   other?.Address == Address &&
                   Phones.SequenceEqual(other?.Phones) &&
                   Emails.SequenceEqual(other?.Emails);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(LastName, FirstName, Address);
        }
    }
    public class Contacts : List<Contact>
    {
        public Contacts(IEnumerable<Contact> contacts) => base.AddRange(contacts);
        public Contacts() { }
        public bool isEmpty => this.Count == 0;

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }

    [Table(Name = "addressbook")]
    public class ContactData
    {
        [Column(Name = "id"), PrimaryKey, Identity]
        public long Id;
        [Column(Name = "aday")]
        public string Aday;
        [Column(Name = "address")]
        public string Address;
        [Column(Name = "address2")]
        public string Address2;
        [Column(Name = "amonth")]
        public string Amonth;
        [Column(Name = "ayear")]
        public string Ayear;
        [Column(Name = "bday")]
        public string Bday;
        [Column(Name = "bmonth")]
        public string Bmonth;
        [Column(Name = "byear")]
        public string Byear;
        [Column(Name = "company")]
        public string Company;
        [Column(Name = "email")]
        public string Email;
        [Column(Name = "email2")]
        public string Email2;
        [Column(Name = "email3")]
        public string Email3;
        [Column(Name = "fax")]
        public string Fax;
        [Column(Name = "firstname")]
        public string Firstname;
        [Column(Name = "home")]
        public string Home;
        [Column(Name = "homepage")]
        public string Homepage;
        [Column(Name = "lastname")]
        public string Lastname;
        [Column(Name = "middlename")]
        public string Middlename;
        [Column(Name = "mobile")]
        public string Mobile;
        public string New_group;
        [Column(Name = "nickname")]
        public string Nickname;
        [Column(Name = "notes")]
        public string Notes;
        [Column(Name = "phone2")]
        public string Phone2;
        [Column(Name = "title")]
        public string Title;
        [Column(Name = "work")]
        public string Work;
        [Column(Name = "deprecated")]
        public string Deprecated;


        //public override string ToString() => this.ToJson();

        public ContactData() { }
        public ContactData(
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
            Title = title;
            Work = work;
        }

        public List<string> GetPhones()
        {
            var result = new string[]
            {
                Home.DigitsOnly(),
                Mobile.DigitsOnly(),
                Work.DigitsOnly(),
                Phone2.DigitsOnly()
            };
            return result.Where(x => x != "").ToList();
        }

        public List<string> GetMails()
        {
            var result = new string[]
            {
                Email,
                Email2,
                Email3
            };
            return result.Where(x => x != "").ToList();
        }

        public static ContactData Random(bool fioIsNull = false)
        {
            ContactData result = new ContactData();
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
            result.Title = fakerRu.Random.Word();
            result.Work = fakerRu.Phone.PhoneNumber();
            return result;
        }
    }
}