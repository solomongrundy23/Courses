using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookAutotests.Models
{
    [Table(Name = "address_in_group")]
    public class GroupContactLink
    {
        [Column(Name = "group_id")]
        public long GroupId;
        [Column(Name = "id")]
        public long Id;
    }
}
