using AddressBook.Api.Models;
using AddressBook.Application.Dtos;
using AutoMapper;

namespace AddressBook.Api.Mapper
{
    public class ApiMapping : Profile
    {
        public ApiMapping()
        {
            CreateMap<CreateContactRequest, ContactDto>();
            CreateMap<UpdateContactRequest, ContactDto>();
            CreateMap<string, ContactTelephoneNumberDto>().ForMember(
                                dest => dest.TelephoneNumber,
                                opt => opt.MapFrom(src => src));
            CreateMap<ContactDto, GetContactResponse>();
            CreateMap<ContactTelephoneNumberDto, ContactTelephoneNumberResponse>();
        }
    }
}
