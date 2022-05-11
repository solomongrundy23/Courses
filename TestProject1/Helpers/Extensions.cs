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
    }
}
