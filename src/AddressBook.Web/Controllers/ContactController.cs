using AddressBook.Application.Dtos;
using AddressBook.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;
using AddressBook.Api.Models;
using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using AddressBook.Api.Paging;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using AddressBook.Application.Exceptions;
using AddressBook.Api.Models.Validators;
using FluentValidation.Results;
using FluentValidation;
using AddressBook.Api.Extensions.ValidationExtensions;
using AddressBook.Api.Hubs;
using Microsoft.AspNetCore.SignalR;
using AddressBook.Domain.Entities;

namespace AddressBook.Api.Controllers
{
    [ApiController]
    [Route("api/contacts")]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly ILogger<ContactController> _logger;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateContactRequest> _createValidator;
        private readonly IValidator<UpdateContactRequest> _updateValidator;
        private readonly IHubContext<ContactHub> _contactHubContext;

        public ContactController(IContactService contactService, ILogger<ContactController> logger, IMapper mapper, IValidator<CreateContactRequest> createValidator, IValidator<UpdateContactRequest> updateValidator, IHubContext<ContactHub> contactHubContext)
        {
            _contactService = contactService;
            _logger = logger;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _contactHubContext = contactHubContext;
        }

        [HttpGet] // api/contacts
        public async Task<IActionResult> GetPaged([FromQuery] PagingRequest request)
        {

            var validRequest = new PagingRequest(request.PageNumber, request.PageSize);
            var (contacts, totalCount) = await _contactService.GetPagedWithTelephoneNumbersAsync(request.PageNumber, request.PageSize);

            if (!contacts.Any())
            {
                return NotFound();
            }

            return Ok(new PagedResponse<GetContactResponse>(_mapper.Map<List<ContactDto>, List<GetContactResponse>>(contacts), totalCount, validRequest.PageNumber, validRequest.PageSize));
        }

        [HttpGet("{id}")] // api/contacts/{id}
        public async Task<IActionResult> GetContactById(int id)
        {

            var contact = await _contactService.GetByIdWithTelephoneNumbersAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ContactDto, GetContactResponse>(contact));

        }

        [HttpPost] // api/contacts
        public async Task<IActionResult> CreateContact([FromBody] CreateContactRequest request)
        {

            ValidationResult result = await _createValidator.ValidateAsync(request);

            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);

                return BadRequest(ModelState);
            }
            var createdContact = await _contactService.CreateAsync(_mapper.Map<CreateContactRequest, ContactDto>(request));
            await _contactHubContext.Clients.All.SendAsync("Created contact with name ", createdContact.Name);

            return CreatedAtAction(nameof(GetContactById), new { id = createdContact.Id }, createdContact);


        }

        [HttpPut("{id}")] // api/contacts/{id}
        public async Task<IActionResult> UpdateContact(int id, [FromBody] UpdateContactRequest request)
        {
            ValidationResult result = await _updateValidator.ValidateAsync(request);

            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);

                return BadRequest(ModelState);
            }
            var existingContact = await _contactService.GetByIdWithTelephoneNumbersAsync(id);

            if (existingContact == null)
            {
                return NotFound();
            }


            var updatedContact = await _contactService.UpdateAsync(id, _mapper.Map<UpdateContactRequest, ContactDto>(request));
            await _contactHubContext.Clients.All.SendAsync("Updated contact with name ", updatedContact.Name);

            return Ok(updatedContact);

        }


        [HttpDelete("{id}")] // api/contacts/{id}
        public async Task<IActionResult> DeleteContact(int id)
        {

            var existingContact = await _contactService.GetByIdWithTelephoneNumbersAsync(id);

            if (existingContact == null)
            {
                return NotFound();
            }

            await _contactService.DeleteAsync(id);
            await _contactHubContext.Clients.All.SendAsync("Deleted contact with name ", existingContact.Name);

            return NoContent();

        }
    }
}
