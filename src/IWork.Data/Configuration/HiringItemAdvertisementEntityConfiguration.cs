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
    public class HiringItemAdvertisementEntityConfiguration : IEntityTypeConfiguration<HiringItemAdvertisement>
    {
        public void Configure(EntityTypeBuilder<HiringItemAdvertisement> builder)
        {
            builder.Property<Guid>("Id")
               .ValueGeneratedOnAdd();

            builder.HasOne<HiringAdvertisement>()
                .WithMany(h => h.Items)
                .HasForeignKey(i => i.HiringAdvertisementId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(i => i.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

        }
    }
}
