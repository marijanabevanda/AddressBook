using System;
using System.Collections.Generic;

namespace AddressBook.Api.Models
{
    public class CreateContactRequest
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<string> TelephoneNumbers { get; set; }
    }
}
