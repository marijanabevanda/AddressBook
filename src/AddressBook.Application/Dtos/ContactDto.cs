using AddressBook.Application.Dtos.Base;
using AddressBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Application.Dtos
{
    public class ContactDto : BaseDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }

        public List<ContactTelephoneNumberDto> TelephoneNumbers { get; set; }
    }
}
