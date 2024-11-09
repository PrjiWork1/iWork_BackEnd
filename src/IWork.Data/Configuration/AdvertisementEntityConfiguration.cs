using IWork.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Data.Configuration
{
    public class AdvertisementEntityConfiguration : IEntityTypeConfiguration<Advertisement>
    {
        public void Configure(EntityTypeBuilder<Advertisement> builder)
        {
            builder.ToTable("Advertisement");

            builder.Property<Guid>("Id")
                 .ValueGeneratedOnAdd();

            builder.Property(a => a.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Description)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(a => a.UrlBanner)
                .HasMaxLength(200);

            builder.Property(a => a.Type)
               .IsRequired()
               .HasConversion<string>()
               .HasMaxLength(50);

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

            builder.Property(a => a.NumberOfSales)
                .IsRequired();

            builder.Property(a => a.Price)
                 .HasColumnType("decimal(18,2)");

            builder.HasOne(a => a.User)
                .WithMany(u => u.Advertisement)
                .HasForeignKey(a => a.UserId)
                .IsRequired();

            builder.HasOne(a => a.Category)
                .WithMany(c => c.Advertisement)
                .HasForeignKey(a => a.CategoryId)
                .IsRequired();
        }
    }
}
