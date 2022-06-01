using AddressBookAutotests.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JsonHelper;
using XMLHelper;
using System.Reflection;

namespace AddressBookAutotests.Helpers
{
    public class FileHelper
    {
        public static string DataSetPath => GetDataSetPath();
        private static string GetDataSetPath()
        {
            var folders = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Split('\\').ToList();
            for (int i = 0; i < 3; i++) folders.RemoveAt(folders.Count() - 1);
            var folder = string.Join("\\", folders);
            return $"{folder}\\AutoTestsCourse\\DataSetsFiles\\";
        }

        public enum FileFormat { Csv, Xml, Json}

        public void Save(List<GroupData> groups, string fileName, FileFormat format)
        {
            var writer = new StreamWriter(fileName);
            switch (format)
            {
                case FileFormat.Csv: SaveToCSV(groups, writer); break;
                case FileFormat.Xml: SaveToXML(groups, writer); break;
                case FileFormat.Json: SaveToJson(groups, writer); break;
                default: throw new FormatException("Unknown format");
            }
            writer.Close();
        }

        public void Save(List<ContactData> contacts, string fileName, FileFormat format)
        {
            var writer = new StreamWriter(fileName);
            switch (format)
            {
                case FileFormat.Csv: SaveToCSV(contacts, writer); break;
                case FileFormat.Xml: SaveToXML(contacts, writer); break;
                case FileFormat.Json: SaveToJson(contacts, writer); break;
                default: throw new FormatException("Unknown format");
            }
            writer.Close();
        }

        public List<GroupData> LoadGroupsFromCSV(StreamReader reader)
        { 
            var result = new List<GroupData>();
            while (!reader.EndOfStream)
            {
                var row = reader.ReadLine().Split(';');
                var obj = new GroupData(row[0], row[1], row[2]);
                result.Add(obj);
            }
            return result;
        }

        public List<GroupData> LoadGroupsFromXML(StreamReader reader)
        {
            return reader.ReadToEnd().FromXML<List<GroupData>>();
        }

        public List<GroupData> LoadGroupsFromJson(StreamReader reader)
        {
            return reader.ReadToEnd().FromJson<List<GroupData>>();
        }

        public List<ContactData> LoadContactFromCSV(StreamReader reader)
        {
            var result = new List<ContactData>();
            while (!reader.EndOfStream)
            {
                var row = reader.ReadLine().Split(';');
                var obj = new ContactData();
                obj.Aday = row[0];
                obj.Address = row[1];
                obj.Address2 = row[2];
                obj.Amonth = row[3];
                obj.Ayear = row[4];
                obj.Bday = row[5];
                obj.Bmonth = row[6];
                obj.Byear = row[7];
                obj.Company = row[8];
                obj.Email = row[9];
                obj.Email2 = row[10];
                obj.Email3 = row[11];
                obj.Fax = row[12];
                obj.Firstname = row[13];
                obj.Home = row[14];
                obj.Homepage = row[15];
                obj.Lastname = row[16];
                obj.Middlename = row[17];
                obj.Mobile = row[18];
                obj.New_group = row[19];
                obj.Nickname = row[20];
                obj.Notes = row[21];
                obj.Phone2 = row[22];
                obj.Title = row[23];
                obj.Work = row[24];
                result.Add(obj);
            }
            return result;
        }

        public List<ContactData> LoadContactFromXML(StreamReader reader)
        {
            return reader.ReadToEnd().FromXML<List<ContactData>>();
        }

        public List<ContactData> LoadContactsFromJson(StreamReader reader)
        {
            return reader.ReadToEnd().FromJson<List<ContactData>>();
        }


        public List<GroupData> LoadGroups(string fileName, FileFormat format)
        {
            var writer = new StreamReader(fileName);
            switch (format)
            {
                case FileFormat.Csv: return LoadGroupsFromCSV(writer);
                case FileFormat.Xml: return LoadGroupsFromXML(writer);
                case FileFormat.Json: return LoadGroupsFromJson(writer);
                default: throw new FormatException("Unknown format");
            }
        }

        public List<ContactData> LoadContacts(string fileName, FileFormat format)
        {
            var writer = new StreamReader(fileName);
            switch (format)
            {
                case FileFormat.Csv: return LoadContactFromCSV(writer);
                case FileFormat.Xml: return LoadContactFromXML(writer);
                case FileFormat.Json: return LoadContactsFromJson(writer);
                default: throw new FormatException("Unknown format");
            }
        }

        private void SaveToCSV(List<GroupData> groups, StreamWriter stream)
        {
            var data = groups.Select(x => $"{x.Name};{x.Header};{x.Footer}").AsString();
            stream.Write(data);
        }

        private void SaveToCSV(List<ContactData> contacts, StreamWriter stream)
        { 
            stream.Write(contacts.Select(x => $"{x.Aday};{x.Address};{x.Address2};{x.Amonth};{x.Ayear};{x.Bday};{x.Bmonth};" +
            $"{x.Byear};{x.Company};{x.Email};{x.Email2};{x.Email3};{x.Fax};{x.Firstname};{x.Home};{x.Homepage};{x.Lastname};" +
            $"{x.Middlename};{x.Mobile};{x.New_group};{x.Nickname};{x.Notes};{x.Phone2};{x.Title};{x.Work}").AsString());
        }

        private void SaveToXML(List<GroupData> obj, StreamWriter stream)
        {
            stream.Write(obj.ToXML());
        }

        private void SaveToXML(List<ContactData> obj, StreamWriter stream)
        {
            stream.Write(obj.ToXML());
        }

        private void SaveToJson(object obj, StreamWriter stream)
        {
            stream.Write(obj.ToJson());
        }
    }
}
