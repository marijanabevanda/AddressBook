using AddressBook.Application.Dtos;
using AddressBook.Application.Interfaces;
using AddressBook.Domain.Entities;
using AddressBook.Domain.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
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
        public async Task CreateAsync(ContactDto contact)
        {
            var createdContact = await _contactRepository.CreateAsync(_mapper.Map<ContactDto, Contact>(contact));
            contact.TelephoneNumbers.ForEach(tn=>tn.ContactId  = createdContact.Id);
            await _contactTelephoneNumberRepository.CreateRangeAsync(_mapper.Map<List<ContactTelephoneNumberDto>, List<ContactTelephoneNumber>>(contact.TelephoneNumbers));
            
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
            var telephoneNumbers = (await _contactTelephoneNumberRepository.GetAllByContactIdAsync(id)).ToList();
            model.TelephoneNumbers = _mapper.Map<List<ContactTelephoneNumber>, List<ContactTelephoneNumberDto>>(telephoneNumbers);
            return model;
        }

        /// <summary>
        /// Retrieves a paged list of contacts, including all their telephone numbers
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve</param>
        /// <param name="pageSize">The number of contacts per page</param>
        /// <returns>The retrieved list of contacts, including telephone numbers</returns>
        public async Task<List<ContactDto>> GetPagedWithTelephoneNumbersAsync(int pageNumber, int pageSize)
        {
            var contacts = (await _contactRepository.GetPagedAsync(pageNumber, pageSize)).ToList();
            return _mapper.Map<List<Contact>, List<ContactDto>>(contacts);
        }


        /// <summary>
        /// Updates the information of the contact with the provided information, including telephone numbers
        /// </summary>
        /// <param name="contact">The updated contact information, including telephone numbers</param>
        public async Task UpdateAsync(ContactDto contact)
        {
            await UpdateTelephoneNumbers(contact);
            await _contactRepository.UpdateAsync(_mapper.Map<ContactDto, Contact>(contact));
        }

        /// <summary>
        /// Updates the telephone numbers associated with the contact in the repository
        /// </summary>
        /// <param name="contact">The updated contact information, including telephone numbers</param>
        private async Task UpdateTelephoneNumbers(ContactDto contact)
        {
            // Retrieve existing telephone numbers for the given contact from the repository
            var existingTelephoneNumbers = await _contactTelephoneNumberRepository.GetAllByContactIdAsync(contact.Id);

            // Extract the IDs of new telephone numbers added
            var newNumberIds = contact.TelephoneNumbers.Select(tn => tn.Id);

            // Identify existing telephone numbers that need to be updated based on whether they have changed or not
            var existingNumbersToUpdate = existingTelephoneNumbers
                .Where(tn => newNumberIds.Contains(tn.Id) && tn.TelephoneNumber != contact.TelephoneNumbers.FirstOrDefault(n => n.Id == tn.Id).TelephoneNumber)
                .Select(tn => _mapper.Map<ContactTelephoneNumberDto, ContactTelephoneNumber>(contact.TelephoneNumbers.FirstOrDefault(n => n.Id == tn.Id)));

            // Update the identified existing telephone numbers in the repository
            await _contactTelephoneNumberRepository.UpdateRangeAsync(existingNumbersToUpdate);

            // Create new telephone numbers associated with the contact
            var newNumbersToCreate = contact.TelephoneNumbers
                    .Where(tn => tn.Id == 0)
                    .Select(tn =>
                    {
                        tn.ContactId = contact.Id;
                        return _mapper.Map<ContactTelephoneNumberDto, ContactTelephoneNumber>(tn);
                    });

            // Add the new telephone numbers to the repository
            await _contactTelephoneNumberRepository.CreateRangeAsync(newNumbersToCreate);

            // Identify existing telephone numbers that are not associated with the contact anymore
            var existingNumbersToDeleteIds = existingTelephoneNumbers
                        .Where(tn => !newNumberIds.Contains(tn.Id))
                          .Select(tn => tn.Id);


            // Delete the identified existing telephone numbers from the repository
            await _contactTelephoneNumberRepository.DeleteRangeAsync(existingNumbersToDeleteIds);
        }
    }
}
