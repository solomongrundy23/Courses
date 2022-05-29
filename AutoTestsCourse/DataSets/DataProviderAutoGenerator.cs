using AddressBookAutotests.Models;
using System.Collections.Generic;

namespace AddressBookAutotests.DataSets
{
    public static class DataProviderAutoGenerator
    {
        public static IEnumerable<GroupData> CreateGroupDatas()
        {
            for (int i = 0; i < 3; i++) yield return GroupData.Random();
        }

        public static IEnumerable<ContactData> CreateContactDatas()
        {
            for (int i = 0; i < 3; i++) yield return ContactData.Random();
        }
    }
}