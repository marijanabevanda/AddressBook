using AddressBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Domain.Interfaces
{
    public interface IContactRepository
    {
        Task<Contact> GetByIdAsync(int id);
        Task<int> GetCountAsync();
        Task<IEnumerable<Contact>> GetPagedAsync(int pageNumber, int pageSize, bool includeTelephoneNumbers = true);
        Task<Contact> CreateAsync(Contact contact);
        Task<Contact> UpdateAsync(Contact contact);
        Task DeleteAsync(int id);
    }
}
