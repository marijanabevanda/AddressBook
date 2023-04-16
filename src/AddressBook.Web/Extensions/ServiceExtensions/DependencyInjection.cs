using AddressBook.Api.Models;
using AddressBook.Api.Models.Validators;
using AddressBook.Application.Interfaces;
using AddressBook.Application.Services;
using AddressBook.Domain.Interfaces;
using AddressBook.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AddressBook.Api.Extensions.ServiceExtensions
{
    public static class DependencyInjection
    {
        //repositories
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {

            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IContactTelephoneNumberRepository, ContactTelephoneNumberRepository>();


            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {

            services.AddScoped<IContactService, ContactService>();


            return services;

        }

        public static IServiceCollection RegisterValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateContactRequest>, CreateContactRequestValidator>();
            services.AddScoped<IValidator<UpdateContactRequest>, UpdateContactRequestValidator>();
            return services;
        }
    }
}
