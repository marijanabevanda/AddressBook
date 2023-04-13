using AddressBook.Domain.Entities;
using AddressBook.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Infrastructure.Repositories
{
    public class ContactTelephoneNumberRepository : IContactTelephoneNumberRepository
    {
        private readonly AddressBookDbContext _dbContext;

        public ContactTelephoneNumberRepository(AddressBookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(ContactTelephoneNumber telephoneNumber)
        {
            await _dbContext.ContactTelephoneNumbers.AddAsync(telephoneNumber);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var contact = await GetByIdAsync(id);
            _dbContext.ContactTelephoneNumbers.Remove(contact);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ContactTelephoneNumber>> GetAllByContactIdAsync(int contactId)
        {
            return await _dbContext.ContactTelephoneNumbers.Where(x => x.ContactId == contactId).ToListAsync();
        }

        public async Task<ContactTelephoneNumber> GetByIdAsync(int id)
        {
            return await _dbContext.ContactTelephoneNumbers.FindAsync(id);
        }

        public async Task UpdateAsync(ContactTelephoneNumber telephoneNumber)
        {
            _dbContext.ContactTelephoneNumbers.Update(telephoneNumber);
            await _dbContext.SaveChangesAsync();
        }
    }
}
