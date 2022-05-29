using Bogus;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using JsonHelper;
using XMLHelper;
using LinqToDB.Mapping;

namespace AddressBookAutotests.Models
{
    public class Groups : List<WebGroup>
    {
        public List<WebGroup> GetList()
        {
            return new List<WebGroup>(this.AsEnumerable());
        }

        public Groups() { }

        public Groups(IEnumerable<WebGroup> groups) => base.AddRange(groups);

        public void Add(string name) => Add(new WebGroup(name));
        public override string ToString() => string.Join(Environment.NewLine, this);
        public WebGroup[] Find(string name) => this.Where(x => x.Name == name).ToArray();
        public WebGroup FindFirst(string name) => this.Where(x => x.Name == name).FirstOrDefault();
    }

    public class WebGroup : IComparable<WebGroup>
    {
        public WebGroup(string name)
        { 
            Name = name;
        }

        public string Name { get; set; }

        public override string ToString() => $"{Name}";

        public int CompareTo(WebGroup other)
        {
            return string.Compare(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is WebGroup)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return ((WebGroup)obj).Name == Name;
        }

        public override int GetHashCode()
        { 
            return Name.GetHashCode();
        }
    }


    [Table(Name = "group_list")]
    public class GroupData
    {
        [Column(Name = "group_name")]
        public string Name { get; set; }
        [Column(Name = "group_header")]
        public string Header { get; set; }
        [Column(Name = "group_footer")]
        public string Footer { get; set; }
        [Column(Name = "group_id"), PrimaryKey, Identity]
        public long Id { get; set; }
        [Column(Name = "deprecated")]
        public string Deprecated;

        public GroupData() { }
        public GroupData(string groupName, string groupHeader, string groupFooter)
        {
            Name = groupName;
            Header = groupHeader;
            Footer = groupFooter;
        }

        //public override string ToString() => this.ToJson();// пришлось убрать из за бага nunit, ждем обновления

        public static GroupData Random()
        {
            var faker = new Faker("ru");
            return new GroupData(faker.Random.Word(), faker.Random.Words(5), faker.Random.Words(10));
        }
    }
}
