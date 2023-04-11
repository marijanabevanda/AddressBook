using AddressBook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Infrastructure.Configurations
{
    public class ContactConfiguration : BaseEntityConfiguration<Contact>
    {
        public override void Configure(EntityTypeBuilder<Contact> builder)
        {
            base.Configure(builder);
            builder.ToTable(nameof(Contact));
            builder.Property(x => x.Name).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.Address).IsRequired(true).HasMaxLength(150);
            builder.Property(x => x.DateOfBirth).IsRequired(true);

            builder.HasIndex(x=>new { x.Name, x.Address }).IsUnique();


            builder.HasMany(x => x.TelephoneNumbers)
                .WithOne(x => x.Contact)
                .HasForeignKey(x => x.ContactId);
        }
    }


}
