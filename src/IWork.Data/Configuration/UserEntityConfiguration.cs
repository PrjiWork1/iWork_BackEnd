using IWork.Domain.Models.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Data.Configuration
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.Property(p => p.CompleteName)
                .IsRequired()
                .HasMaxLength(150);
            
            builder.Property(p => p.UserName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.CPF)
                .IsRequired()
                .HasMaxLength(14);

            builder.Property(p => p.BirthDate)
                .IsRequired();

            builder.Property(p => p.PhoneNumber)
                .HasMaxLength(20);

            builder.Property(p => p.PasswordHash)
                .IsRequired();

        }
    }
}
