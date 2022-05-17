using AddressBookAutotests.Models;
using AddressBookAutotests.Helpers;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

namespace AddressBookAutotests.DataSets
{
    public static class DataProviderFromFile
    {
        private static readonly string _groupPath = _groupPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\DataSetFiles\\groups.";
        private static readonly string _contactsPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\DataSetFiles\\contacts.";

        public static IEnumerable<CreateGroupData> CreateGroupDatasFromCSV()
        {
            return new FileHelper().LoadGroups(_groupPath + "csv", FileHelper.FileFormat.Csv);
        }

        public static IEnumerable<CreateGroupData> CreateGroupDatasFromXML()
        {
            return new FileHelper().LoadGroups(_groupPath + "xml", FileHelper.FileFormat.Xml);
        }

        public static IEnumerable<CreateGroupData> CreateGroupDatasFromJson()
        {
            return new FileHelper().LoadGroups(_groupPath + "json", FileHelper.FileFormat.Json);
        }

        public static IEnumerable<CreateContactData> CreatContactDatasFromCSV()
        {
            return new FileHelper().LoadContacts(_contactsPath + "csv", FileHelper.FileFormat.Csv);
        }

        public static IEnumerable<CreateContactData> CreateContactDatasFromXML()
        {
            return new FileHelper().LoadContacts(_contactsPath + "xml", FileHelper.FileFormat.Xml);
        }

        public static IEnumerable<CreateContactData> CreateContactDatasFromJson()
        {
            return new FileHelper().LoadContacts(_contactsPath + "json", FileHelper.FileFormat.Json);
        }
    }
}
