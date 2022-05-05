using Bogus;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AddressBookAutotests.Models
{
    public class ReturnedGroups : List<ReturnedGroup>
    {
        public void Add(IWebElement webElement, string value, string text) => Add(new ReturnedGroup(webElement, value, text));
        public override string ToString() => string.Join(Environment.NewLine, this);
        public ReturnedGroup Random()
        {
            if (Count == 0) throw new Exception("ReturnedGroups is Empty");
            return this[new Random().Next(Count - 1)];
        }
        public ReturnedGroup[] Find(string text) => this.Where(x => x.Text == text).ToArray();
        public ReturnedGroup? FindFirst(string text) => this.Where(x => x.Text == text).FirstOrDefault();
    }
    public class ReturnedGroup : IComparable<ReturnedGroup>, IEquatable<ReturnedGroup>
    {
        public ReturnedGroup(IWebElement webElement, string value, string text)
        { 
            Value = value;
            Text = text;
            WebElement = webElement;
        }

        public string Value { get; set; }
        public string Text { get; set; }
        public IWebElement WebElement { get; set; }

        public override string ToString() => $"{Value}: {Text}";

        public int CompareTo(ReturnedGroup? other)
        {
            return string.Compare(other?.Text, Text);
        }

        public bool Equals(ReturnedGroup? other)
        {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Text == Text;
        }

        public override int GetHashCode()
        { 
            return Text.GetHashCode();
        }
    }
    public class CreateGroupData
    {
        public string? Name { get; set; }
        public string? Header { get; set; }
        public string? Footer { get; set; }

        public CreateGroupData() { }
        public CreateGroupData(string groupName, string groupHeader, string groupFooter)
        {
            Name = groupName;
            Header = groupHeader;
            Footer = groupFooter;
        }

        public static CreateGroupData Random()
        {
            var faker = new Faker("ru");
            return new CreateGroupData(faker.Random.Word(), faker.Random.Words(5), faker.Random.Words(10));
        }
    }
}
