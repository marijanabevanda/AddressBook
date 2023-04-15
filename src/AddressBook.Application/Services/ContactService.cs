﻿using AddressBook.Application.Dtos;
using AddressBook.Application.Interfaces;
using AddressBook.Domain.Entities;
using AddressBook.Domain.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Application.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IContactTelephoneNumberRepository _contactTelephoneNumberRepository;
        private readonly IMapper _mapper;


        public ContactService(IContactRepository contactRepository, IContactTelephoneNumberRepository contactTelephoneNumberRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _contactTelephoneNumberRepository = contactTelephoneNumberRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new contact with the provided information, including telephone numbers
        /// </summary>
        /// <param name="contact">The contact information to create</param>
        public async Task<ContactDto> CreateAsync(ContactDto contact)
        {
            var createdContact = await _contactRepository.CreateAsync(_mapper.Map<ContactDto, Contact>(contact));
            return _mapper.Map<Contact, ContactDto>(createdContact);    
        }

        /// <summary>
        /// Deletes the contact with the provided ID, including all its telephone numbers
        /// </summary>
        /// <param name="id">The ID of the contact to delete</param>
        public async Task DeleteAsync(int id)
        {
            var telephoneNumbers = await _contactTelephoneNumberRepository.GetAllByContactIdAsync(id);
            await _contactTelephoneNumberRepository.DeleteRangeAsync(telephoneNumbers.Select(x => x.Id));
            await _contactRepository.DeleteAsync(id);
        }

        /// <summary>
        /// Retrieves the contact with the provided ID, including all its telephone numbers
        /// </summary>
        /// <param name="id">The ID of the contact to retrieve</param>
        /// <returns>The retrieved contact information, including telephone numbers</returns>
        public async Task<ContactDto> GetByIdWithTelephoneNumbersAsync(int id)
        {
            var contact = await _contactRepository.GetByIdAsync(id);
            var model = _mapper.Map<Contact, ContactDto>(contact);
            if (model != null)
            {
                var telephoneNumbers = (await _contactTelephoneNumberRepository.GetAllByContactIdAsync(id)).ToList();
                model.TelephoneNumbers = _mapper.Map<List<ContactTelephoneNumber>, List<ContactTelephoneNumberDto>>(telephoneNumbers);
            }
            return model;
        }

        /// <summary>
        /// Retrieves a paged list of contacts, including all their telephone numbers
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve</param>
        /// <param name="pageSize">The number of contacts per page</param>
        /// <returns>The retrieved list of contacts, including telephone numbers</returns>
        public async Task<(List<ContactDto> Contacts, int TotalCount)> GetPagedWithTelephoneNumbersAsync(int pageNumber, int pageSize)
        {
            var contacts = (await _contactRepository.GetPagedAsync(pageNumber, pageSize)).ToList();
            var totalCount = await _contactRepository.GetCountAsync();
            return (_mapper.Map<List<Contact>, List<ContactDto>>(contacts), totalCount);
        }


        /// <summary>
        /// Updates the information of the contact with the provided information, including telephone numbers
        /// </summary>
        /// <param name="contact">The updated contact information, including telephone numbers</param>
        public async Task<ContactDto> UpdateAsync(int id, ContactDto contact)
        {
            contact.Id = id;
            var existingTelephoneNumbers = await _contactTelephoneNumberRepository.GetAllByContactIdAsync(contact.Id);
            await _contactTelephoneNumberRepository.DeleteRangeAsync(existingTelephoneNumbers.Select(x => x.Id));
            var updatedContact = await _contactRepository.UpdateAsync(_mapper.Map<ContactDto, Contact>(contact));
            return _mapper.Map<Contact, ContactDto>(updatedContact); ;
        }

        
    }
}
