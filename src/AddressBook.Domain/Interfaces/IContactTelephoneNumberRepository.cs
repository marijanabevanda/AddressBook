using AddressBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Domain.Interfaces
{
    public interface IContactTelephoneNumberRepository
    {
        Task<IEnumerable<ContactTelephoneNumber>> GetAllByContactIdAsync(int contactId);
        Task CreateAsync(ContactTelephoneNumber telephoneNumber);
        Task UpdateAsync(ContactTelephoneNumber telephoneNumber);
        Task DeleteAsync(int id);
        Task<ContactTelephoneNumber> GetByIdAsync(int id);
    }
}
