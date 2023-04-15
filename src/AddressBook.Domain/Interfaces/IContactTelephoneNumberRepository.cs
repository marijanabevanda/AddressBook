using AddressBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AddressBook.Domain.Interfaces
{
    public interface IContactTelephoneNumberRepository
    {
        Task<IEnumerable<ContactTelephoneNumber>> GetAllByContactIdAsync(int contactId);
        Task CreateRangeAsync(IEnumerable<ContactTelephoneNumber> telephoneNumbers);
        Task UpdateRangeAsync(IEnumerable<ContactTelephoneNumber> telephoneNumbers);
        Task DeleteRangeAsync(IEnumerable<int> telephoneNumbersIds);
    }
}
