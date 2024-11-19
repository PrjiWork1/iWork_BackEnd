using IWork.Domain.Models;
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
    public class HiringAdvertisementEntityConfiguration : IEntityTypeConfiguration<HiringAdvertisement>
    {
        public void Configure(EntityTypeBuilder<HiringAdvertisement> builder)
        {
            builder.Property<Guid>("Id")
               .ValueGeneratedOnAdd();

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(h => h.AdvertiserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(h => h.ContractorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Advertisement>()
                .WithMany()
                .HasForeignKey(h => h.AdvertisementId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(h => h.PreferenceId)
               .HasMaxLength(60);
            
            builder.Property(h => h.Description)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasMany(h => h.Items)
                .WithOne()
                .HasForeignKey(i => i.HiringAdvertisementId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(h => h.ContractDate)
                .IsRequired();

            builder.Property(h => h.AdvertisementTemplate)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.Property(h => h.AdvertisementType)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.Property(h => h.HiringStatus)
               .IsRequired()
               .HasConversion<string>()
               .HasMaxLength(50);

            builder.Property(h => h.Price)
                .HasColumnType("decimal(18,2)");

            builder.Property(h => h.AdvertisementRate)
                .HasColumnType("decimal(5,4)");

            builder.Property(h => h.TotalAmount)
                .HasColumnType("decimal(18,2)");

            builder.Property(h => h.IsActive)
                .IsRequired();
        }
    }
}
