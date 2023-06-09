﻿using AddressBook.Application.Dtos;
using System.Collections.Generic;
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
        /// <returns>The retrieved list of contacts, including telephone numbers, and the total count of contacts</returns>
        Task<(List<ContactDto> Contacts, int TotalCount)> GetPagedWithTelephoneNumbersAsync(int pageNumber, int pageSize);

        /// <summary>
        /// Creates a new contact with the provided information, including telephone numbers
        /// </summary>
        /// <param name="contact">The contact information to create</param>
        /// <returns>The created contact information, including telephone numbers</returns>
        Task<ContactDto> CreateAsync(ContactDto contact);

        /// <summary>
        /// Updates the information of the contact with the provided information, including telephone numbers
        /// </summary>
        /// <param name="id">The ID of the contact to update</param>
        /// <param name="contact">The updated contact information, including telephone numbers</param>
        /// <returns>The updated contact information, including telephone numbers</returns>
        Task<ContactDto> UpdateAsync(int id, ContactDto contact);

        /// <summary>
        /// Deletes the contact with the provided ID, including all its telephone numbers
        /// </summary>
        /// <param name="id">The ID of the contact to delete</param>
        Task DeleteAsync(int id);
    }
}
