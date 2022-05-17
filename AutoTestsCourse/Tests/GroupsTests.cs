using AddressBookAutotests.DataSets;
using AddressBookAutotests.Models;
using NUnit.Framework;

namespace AddressBookAutotests.Tests
{
    [TestFixture]
    public class GroupsTest : TestWithAuth
    {
        [Test, TestCaseSource(typeof(DataProviderAutoGenerator), nameof(DataProviderAutoGenerator.CreateGroupDatas))]
        [Order(1)]
        [Description("Add new group")]
        public void AddGroupTest(CreateGroupData groupData)
        {
            Manager.Scenarios.AddGroup(groupData);
        }

        [Test, TestCaseSource(typeof(DataProviderFromFile), nameof(DataProviderFromFile.CreateGroupDatasFromCSV))]
        [Order(1)]
        [Description("Add new group from CSV")]
        public void AddGroupTestCsv(CreateGroupData groupData)
        {
            Manager.Scenarios.AddGroup(groupData);
        }

        [Test, TestCaseSource(typeof(DataProviderFromFile), nameof(DataProviderFromFile.CreateGroupDatasFromXML))]
        [Order(1)]
        [Description("Add new group from XML")]
        public void AddGroupTestXml(CreateGroupData groupData)
        {
            Manager.Scenarios.AddGroup(groupData);
        }

        [Test, TestCaseSource(typeof(DataProviderFromFile), nameof(DataProviderFromFile.CreateGroupDatasFromJson))]
        [Order(1)]
        [Description("Add new group from JSON")]
        public void AddGroupTestJson(CreateGroupData groupData)
        {
            Manager.Scenarios.AddGroup(groupData);
        }

        [Test]
        [Order(2)]
        [Description("Edit group")]
        public void EditGroupTest()
        {
            Manager.Scenarios.EditGroup();
        }

        [Test]
        [Order(3)]
        [Description("Remove group")]
        public void RemoveGroupTest()
        {
            Manager.Scenarios.RemoveGroup();
        }
    }
}