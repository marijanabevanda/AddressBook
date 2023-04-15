using AddressBook.Application.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Application.Dtos
{
    public class ContactTelephoneNumberDto : BaseDto
    {
        public int ContactId { get; set; }
        public string TelephoneNumber { get; set; }
    }
}
