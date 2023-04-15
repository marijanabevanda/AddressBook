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
    public class ContactRepository : IContactRepository
    {
        private readonly AddressBookDbContext _dbContext;

        public ContactRepository(AddressBookDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Contact> CreateAsync(Contact contact)
        {
            await _dbContext.Contacts.AddAsync(contact);
            await _dbContext.SaveChangesAsync();
            return contact;
        }

        public async Task DeleteAsync(int id)
        {
            var contact = await GetByIdAsync(id);
            _dbContext.Contacts.Remove(contact);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Contact> GetByIdAsync(int id)
        {
            return await _dbContext.Contacts.AsNoTracking().FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<int> GetCountAsync()
        {
            return await _dbContext.Contacts.CountAsync();
        }

        public async Task<IEnumerable<Contact>> GetPagedAsync(int pageNumber, int pageSize, bool includeTelephoneNumbers = false)
        {

            var query = _dbContext.Contacts
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            if (includeTelephoneNumbers)
            {
                query = query.Include(x => x.TelephoneNumbers);
            }

            return await query.ToListAsync();
        }

        public async Task<Contact> UpdateAsync(Contact contact)
        {
             _dbContext.Contacts.Update(contact);
            await _dbContext.SaveChangesAsync();
            return contact;
        }
    }
}
