using AddressBookAutotests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookAutotests.Helpers
{
    public static class Extensions
    {
        public static string CombineParams(string splitter, params string[] paramList)
        {
            return string.Join(splitter, paramList.Select(x => x ?? string.Empty));
        }

        public static T Random<T>(this List<T> list)
        {
            if (list.Count == 0) throw new Exception("List is Empty");
            return list[new Random().Next(list.Count - 1)];
        }

        public static string DigitsOnly(this string text)
        {
            string result = "";
            foreach (char ch in text)
            {
                if ("1234567890".Contains(ch)) result += ch;
            }
            return result;
        }

        public static string AsString<T>(this List<T> list, string splitter = "\r\n") => string.Join(splitter, list);
        public static string AsString<T>(this IEnumerable<T> list, string splitter = "\r\n") => string.Join(splitter, list);

        public static Groups ToGroups(this IEnumerable<WebGroup> list)
        {
            return new Groups(list);
        }

        public static Contacts ToContacts(this IEnumerable<Contact> list)
        {
            return new Contacts(list);
        }

        public static string FirstLetterToUpperCase(this string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            var firstChar = s[0].ToString().ToUpper();
            if (s.Length > 1)
                return firstChar + s.Substring(1);
            else
                return firstChar.ToString();
        }
    }
}
