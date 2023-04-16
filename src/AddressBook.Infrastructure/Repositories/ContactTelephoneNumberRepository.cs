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


        public async Task CreateRangeAsync(IEnumerable<ContactTelephoneNumber> telephoneNumbers)
        {
            await _dbContext.ContactTelephoneNumbers.AddRangeAsync(telephoneNumbers);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<int> telephoneNumbersIds)
        {
            var telephoneNumbersToDelete = await _dbContext.ContactTelephoneNumbers.Where(tn => telephoneNumbersIds.Contains(tn.Id)).ToListAsync();
            _dbContext.ContactTelephoneNumbers.RemoveRange(telephoneNumbersToDelete);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ContactTelephoneNumber>> GetAllByContactIdAsync(int contactId)
        {
            return await _dbContext.ContactTelephoneNumbers.Where(x => x.ContactId == contactId).ToListAsync();
        }

        public async Task<bool> IsDuplicateAsync(IEnumerable<string> telephoneNumbers, int? excludedContactId = null)
        {
           return await _dbContext.ContactTelephoneNumbers
                .Where(x=>x.ContactId != excludedContactId)
                .AnyAsync(x => telephoneNumbers.Contains(x.TelephoneNumber));
        }

        public async Task UpdateRangeAsync(IEnumerable<ContactTelephoneNumber> telephoneNumbers)
        {
            _dbContext.ContactTelephoneNumbers.UpdateRange(telephoneNumbers);
            await _dbContext.SaveChangesAsync();
        }
    }
}
