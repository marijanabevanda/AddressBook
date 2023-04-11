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
    public class ContactTelephoneNumberConfiguration : BaseEntityConfiguration<ContactTelephoneNumber>
    {
        public override void Configure(EntityTypeBuilder<ContactTelephoneNumber> builder)
        {
            base.Configure(builder);
            builder.ToTable(nameof(ContactTelephoneNumber));
            builder.Property(x => x.TelephoneNumber).IsRequired(true).HasMaxLength(15);
            builder.HasOne(x => x.Contact)
                .WithMany(x => x.TelephoneNumbers)
                .HasForeignKey(x => x.ContactId)
                .IsRequired(true);

        }
    }
}
