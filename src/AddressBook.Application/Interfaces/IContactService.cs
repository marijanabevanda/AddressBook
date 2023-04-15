using AddressBook.Application.Dtos;
using AddressBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Application.Interfaces
{
    public interface IContactService
    {
        /// <summary>
        /// Retrieves the contact with the provided ID, including all its telephone numbers
        /// </summary>
        /// <param name="id">The ID of the contact to retrieve</param>
        /// <returns>The retrieved contact information, including telephone numbers</returns>
        Task<ContactDto> GetByIdWithTelephoneNumbersAsync(int id);

        /// <summary>
        /// Retrieves a paged list of contacts, including all their telephone numbers
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve</param>
        /// <param name="pageSize">The number of contacts per page</param>
        /// <returns>The retrieved list of contacts, including telephone numbers</returns>
        Task<List<ContactDto>> GetPagedWithTelephoneNumbersAsync(int pageNumber, int pageSize);

        /// <summary>
        /// Creates a new contact with the provided information, including telephone numbers
        /// </summary>
        /// <param name="contact">The contact information to create</param>
        Task CreateAsync(ContactDto contact);

        /// <summary>
        /// Updates the information of the contact with the provided information, including telephone numbers
        /// </summary>
        /// <param name="contact">The updated contact information, including telephone numbers</param>
        Task UpdateAsync(ContactDto contact);

        /// <summary>
        /// Deletes the contact with the provided ID, including all its telephone numbers
        /// </summary>
        /// <param name="id">The ID of the contact to delete</param>
        Task DeleteAsync(int id);
    }
}
