using AddressBookAutotests.Models;
using AddressBookAutotests.Helpers;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

namespace AddressBookAutotests.DataSets
{
    public static class DataProviderFromFile
    {
        private static readonly string _groupPath = _groupPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\DataSetsFiles\\groups.";
        private static readonly string _contactsPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\DataSetsFiles\\contacts.";

        public static IEnumerable<GroupData> CreateGroupDatasFromCSV()
        {
            return new FileHelper().LoadGroups(_groupPath + "csv", FileHelper.FileFormat.Csv);
        }

        public static IEnumerable<GroupData> CreateGroupDatasFromXML()
        {
            return new FileHelper().LoadGroups(_groupPath + "xml", FileHelper.FileFormat.Xml);
        }

        public static IEnumerable<GroupData> CreateGroupDatasFromJson()
        {
            return new FileHelper().LoadGroups(_groupPath + "json", FileHelper.FileFormat.Json);
        }

        public static IEnumerable<ContactData> CreatContactDatasFromCSV()
        {
            return new FileHelper().LoadContacts(_contactsPath + "csv", FileHelper.FileFormat.Csv);
        }

        public static IEnumerable<ContactData> CreateContactDatasFromXML()
        {
            return new FileHelper().LoadContacts(_contactsPath + "xml", FileHelper.FileFormat.Xml);
        }

        public static IEnumerable<ContactData> CreateContactDatasFromJson()
        {
            return new FileHelper().LoadContacts(_contactsPath + "json", FileHelper.FileFormat.Json);
        }
    }
}
