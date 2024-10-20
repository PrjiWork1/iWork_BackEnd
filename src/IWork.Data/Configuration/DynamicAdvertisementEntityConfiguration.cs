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
    public class DynamicAdvertisementEntityConfiguration : IEntityTypeConfiguration<DynamicAdvertisement>
    {
        public void Configure(EntityTypeBuilder<DynamicAdvertisement> builder)
        {
           
            builder.Property<Guid>("Id")
                   .ValueGeneratedOnAdd();

            builder.Property(a => a.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Description)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(a => a.UrlBanner)
                .HasMaxLength(100);

            builder.Property(a => a.Type)
               .IsRequired()
               .HasConversion<string>()
               .HasMaxLength(50);

            builder.Property(a => a.IWorkPro)
               .IsRequired();

            builder.Property(a => a.IsActive)
                    .IsRequired();

            builder.Property(a => a.AdvertisementRate)
               .IsRequired()
               .HasColumnType("decimal(18,2)");

            builder.Property(a => a.CreatedAt)
                .IsRequired();

            builder.Property(a => a.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

            builder.HasOne(a => a.User)
                .WithMany(u => u.DynamicAdvertisements)
                .HasForeignKey(a => a.UserId)
                .IsRequired();

            builder.HasOne(a => a.Category)
                .WithMany(c => c.DynamicAdvertisements)
                .HasForeignKey(a => a.CategoryId)
                .IsRequired();
        }
    }
}
