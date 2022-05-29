using AddressBookAutotests.Models;
using LinqToDB;
using LinqToDB.Mapping;

namespace AddressBookAutotests.Models
{
    public class AddressBookDB : LinqToDB.Data.DataConnection
    {
        public AddressBookDB() : base("AddressBook") { }

        public ITable<GroupData> groupDatas => this.GetTable<GroupData>();
        public ITable<ContactData> contactDatas => this.GetTable<ContactData>();
        public ITable<GroupContactLink> groupContantLinks => this.GetTable<GroupContactLink>();
    }
}
