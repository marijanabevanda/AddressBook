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

namespace AddressBook.Api.Controllers
{
    [ApiController]
    [Route("api/contacts")]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly ILogger<ContactController> _logger;
        private readonly IMapper _mapper;

        public ContactController(IContactService contactService, ILogger<ContactController> logger, IMapper mapper)
        {
            _contactService = contactService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaged([FromQuery] PagingRequest request)
        {
            try
            {
                var validRequest = new PagingRequest(request.PageNumber, request.PageSize);
                var (contacts, totalCount) = await _contactService.GetPagedWithTelephoneNumbersAsync(request.PageNumber, request.PageSize);

                if (!contacts.Any())
                {
                    return NotFound();
                }
           
                return Ok(new PagedResponse<GetContactResponse>(_mapper.Map<List<ContactDto>, List<GetContactResponse>>(contacts), totalCount, validRequest.PageNumber, validRequest.PageSize));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to get contacts.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactById(int id)
        {
            try
            {
                var contact = await _contactService.GetByIdWithTelephoneNumbersAsync(id);

                if (contact == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<ContactDto, GetContactResponse>(contact));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to get contact with id: {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact([FromBody] CreateContactRequest request)
        {
            try
            {
                
                var createdContact = await _contactService.CreateAsync(_mapper.Map<CreateContactRequest, ContactDto>(request));

                return CreatedAtAction(nameof(GetContactById), new { id = createdContact.Id }, createdContact);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create contact.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, [FromBody] UpdateContactRequest request)
        {
            try
            {
                var existingContact = await _contactService.GetByIdWithTelephoneNumbersAsync(id);

                if (existingContact == null)
                {
                    return NotFound();
                }

                
                var updatedContact = await _contactService.UpdateAsync(id, _mapper.Map<UpdateContactRequest, ContactDto>(request));
                
                return Ok(updatedContact);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to update contact with id: {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            try
            {
                var existingContact = await _contactService.GetByIdWithTelephoneNumbersAsync(id);

                if (existingContact == null)
                {
                    return NotFound();
                }

                await _contactService.DeleteAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to delete contact with id: {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
