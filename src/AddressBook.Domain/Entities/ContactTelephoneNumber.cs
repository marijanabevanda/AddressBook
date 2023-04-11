using AddressBook.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Domain.Entities
{
    public class ContactTelephoneNumber : BaseEntity
    {
        public Contact Contact { get; set; }
        public int ContactId { get; set; }
        public string TelephoneNumber { get; set;}
    }
}
