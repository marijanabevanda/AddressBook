using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Infrastructure
{
    public static class AddressBookDbContextConfiguration
    {

        public static IServiceCollection AddConfiguredDbContext(this IServiceCollection services, IConfiguration configuration, bool isDevEnvironment)
        {
            var connectionString = configuration.GetConnectionString(nameof(AddressBookDbContext));


            services.AddDbContext<AddressBookDbContext>(options =>
            {
                options.UseNpgsql(connectionString);

                if (isDevEnvironment)
                {
                    options.EnableDetailedErrors(true);
                    options.EnableSensitiveDataLogging(true);
                }
            });

            return services;
        }
    }
}
