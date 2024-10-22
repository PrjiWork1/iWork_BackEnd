using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using IWork.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IWork.Data.Configuration
{
    public class ItemAdvertisementEntityConfiguration : IEntityTypeConfiguration<ItemAdvertisement>
    {
        public void Configure(EntityTypeBuilder<ItemAdvertisement> builder)
        {
            builder.Property<Guid>("Id")
                .ValueGeneratedOnAdd();

            builder.Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(100);
            
            builder.Property(i => i.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.HasOne(i => i.DynamicAdvertisement)
               .WithMany(d => d.Items)
               .HasForeignKey(i => i.DynamicAdvertisementId);
        }
    }
}