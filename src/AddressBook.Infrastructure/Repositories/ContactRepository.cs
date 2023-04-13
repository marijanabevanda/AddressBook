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
        public async Task CreateAsync(Contact contact)
        {
            await _dbContext.Contacts.AddAsync(contact);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var contact = await GetByIdAsync(id);
            _dbContext.Contacts.Remove(contact);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            return await _dbContext.Contacts.ToListAsync();
        }

        public async Task<Contact> GetByIdAsync(int id)
        {
            return await _dbContext.Contacts.FindAsync(id);
        }

        public async Task<IEnumerable<Contact>> GetPagedAsync(int pageNumber, int pageSize)
        {
            return await _dbContext.Contacts
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
        }

        public async Task UpdateAsync(Contact contact)
        {
            _dbContext.Contacts.Update(contact);
            await _dbContext.SaveChangesAsync();
        }
    }
}
