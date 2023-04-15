using AddressBook.Application.Interfaces;
using AddressBook.Application.Services;
using AddressBook.Domain.Interfaces;
using AddressBook.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AddressBook.Api.Extensions.ServiceExtensions
{
    public static class DependencyInjection
    {
        //repositories
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {

            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IContactTelephoneNumberRepository, ContactTelephoneNumberRepository>();


            return services;
        }

        public static IServiceCollection AddDtoServices(this IServiceCollection services)
        {

            services.AddScoped<IContactService, ContactService>();


            return services;

        }
    }
}
