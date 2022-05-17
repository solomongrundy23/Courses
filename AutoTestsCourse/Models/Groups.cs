using Bogus;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using JsonHelper;
using XMLHelper;

namespace AddressBookAutotests.Models
{
    public class Groups : List<Group>
    {
        public void Add(string name) => Add(new Group(name));
        public override string ToString() => string.Join(Environment.NewLine, this);
        public Group Random()
        {
            if (Count == 0) throw new Exception("ReturnedGroups is Empty");
            return this[new Random().Next(Count - 1)];
        }
        public Group[] Find(string name) => this.Where(x => x.Name == name).ToArray();
        public Group FindFirst(string name) => this.Where(x => x.Name == name).FirstOrDefault();
    }
    public class Group : IComparable<Group>
    {
        public Group(string name)
        { 
            Name = name;
        }

        public string Name { get; set; }

        public override string ToString() => $"{Name}";

        public int CompareTo(Group other)
        {
            return string.Compare(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is Group)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return ((Group)obj).Name == Name;
        }

        public override int GetHashCode()
        { 
            return Name.GetHashCode();
        }
    }
    public class CreateGroupData
    {
        public string Name { get; set; }
        public string Header { get; set; }
        public string Footer { get; set; }

        public CreateGroupData() { }
        public CreateGroupData(string groupName, string groupHeader, string groupFooter)
        {
            Name = groupName;
            Header = groupHeader;
            Footer = groupFooter;
        }

        //public override string ToString() => this.ToJson();// пришлось убрать из за бага nunit, ждем обновления

        public static CreateGroupData Random()
        {
            var faker = new Faker("ru");
            return new CreateGroupData(faker.Random.Word(), faker.Random.Words(5), faker.Random.Words(10));
        }
    }
}
