using AddressBook.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Domain.Entities
{
    public class Contact : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }

        public ICollection<ContactTelephoneNumber> TelephoneNumbers { get; set; }   

    }

}
