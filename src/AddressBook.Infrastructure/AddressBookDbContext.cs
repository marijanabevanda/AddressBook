using AddressBook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Infrastructure
{
    public class AddressBookDbContext : DbContext
    {
        public AddressBookDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AddressBookDbContext).Assembly);
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactTelephoneNumber> ContactTelephoneNumbers { get; set; }

    }
}
