using AddressBookAutotests.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;

namespace AddressBookAutotests.DataSets
{
    public static class DataProvider
    {
        public static IEnumerable<CreateGroupData> CreateGroupDatas()
        {
            for (int i = 0; i < 3; i++) yield return CreateGroupData.Random();
        }

        public static IEnumerable<CreateContactData> CreateContactDatas()
        {
            for (int i = 0; i < 3; i++) yield return CreateContactData.Random();
        }
    }
}