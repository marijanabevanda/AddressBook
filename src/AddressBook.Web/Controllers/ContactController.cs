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

        public ContactController(IContactService contactService, ILogger<ContactController> logger, IMapper mapper, IValidator<CreateContactRequest> createValidator, IValidator<UpdateContactRequest> updateValidator)
        {
            _contactService = contactService;
            _logger = logger;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactById(int id)
        {

            var contact = await _contactService.GetByIdWithTelephoneNumbersAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ContactDto, GetContactResponse>(contact));

        }

        [HttpPost]
        public async Task<IActionResult> CreateContact([FromBody] CreateContactRequest request)
        {

            ValidationResult result = await _createValidator.ValidateAsync(request);

            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);

                return BadRequest(ModelState);
            }
            var createdContact = await _contactService.CreateAsync(_mapper.Map<CreateContactRequest, ContactDto>(request));

            return CreatedAtAction(nameof(GetContactById), new { id = createdContact.Id }, createdContact);


        }

        [HttpPut("{id}")]
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

            return Ok(updatedContact);

        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {

            var existingContact = await _contactService.GetByIdWithTelephoneNumbersAsync(id);

            if (existingContact == null)
            {
                return NotFound();
            }

            await _contactService.DeleteAsync(id);

            return NoContent();

        }
    }
}
