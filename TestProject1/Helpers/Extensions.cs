using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookAutotests.Helpers
{
    public static class Extensions
    {
        public static string DigitsOnly(this string text)
        {
            string result = "";
            foreach (char ch in text)
            {
                if ("1234567890".Contains(ch)) result += ch;
            }
            return result;
        }

        public static string FirstLetterToUpperCase(this string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            char firstChar = s[0];
            if (s.Length > 1)
                return firstChar + s.Substring(1);
            else
                return firstChar.ToString();
        }
    }
}
