﻿using AddressBookAutotests.Models;
using System.Collections.Generic;

namespace AddressBookAutotests.DataSets
{
    public static class DataProviderAutoGenerator
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