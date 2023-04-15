using System.Collections.Generic;
using System;

namespace AddressBook.Api.Models
{
    public class UpdateContactRequest
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<string> TelephoneNumbers { get; set; }
    }
}
