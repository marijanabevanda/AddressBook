using System.Collections.Generic;
using System;

namespace AddressBook.Api.Models
{
    public class GetContactResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }

        public List<ContactTelephoneNumberResponse> TelephoneNumbers { get; set; }
    }
}
