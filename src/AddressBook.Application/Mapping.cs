using AddressBook.Application.Dtos;
using AddressBook.Domain.Entities;
using AutoMapper;

namespace AddressBook.Application
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Contact, ContactDto>().ReverseMap();
            CreateMap<ContactTelephoneNumber, ContactTelephoneNumberDto>().ReverseMap();

        }
    }
}
