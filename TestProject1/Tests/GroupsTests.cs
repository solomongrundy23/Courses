using AddressBookAutotests.DataSets;
using AddressBookAutotests.Models;
using NUnit.Framework;

namespace AddressBookAutotests.Tests
{
    [TestFixture]
    public class GroupsTest : TestWithAuth
    {
        [Test, TestCaseSource(typeof(DataProvider), nameof(DataProvider.CreateGroupDatas))]
        [Order(1)]
        [Description("Add new group")]
        public void AddGroupTest(CreateGroupData groupData)
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