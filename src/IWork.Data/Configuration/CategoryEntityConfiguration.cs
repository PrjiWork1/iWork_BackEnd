using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IWork.Domain.Models;

namespace IWork.Data.Configuration
{
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");

            builder.Property<Guid>("Id")
                   .ValueGeneratedOnAdd();

            builder.Property(c => c.Description)
                   .IsRequired()
                   .HasMaxLength(20);
        }
    }
}
