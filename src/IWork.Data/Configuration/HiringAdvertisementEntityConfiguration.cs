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

            // Configura o relacionamento com o usuário (anunciante e contratante)
            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(h => h.AdvertiserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(h => h.ContractorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configura o relacionamento com HiringItemAdvertisement
            builder.HasMany(h => h.Items)
                .WithOne()
                .HasForeignKey(i => i.HiringAdvertisementId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configura outras propriedades
            builder.Property(h => h.ContractDate)
                .IsRequired();
            
            builder.Property(h => h.HiringStatus)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.Property(h => h.AdvertisementTemplate)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.Property(h => h.AdvertisementType)
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
