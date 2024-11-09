using IWork.Domain.Models.IdentityEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IWork.Data.Configuration;
using IWork.Domain.Models;
using IWork.Domain.Models.Enums;

namespace IWork.Data.Context
{
    public class DataContext : IdentityDbContext<User, Role, string,
            IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>,
            IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Advertisement> Advertisement { get; set; }
        public DbSet<ItemAdvertisement> ItemAdvertisement{ get; set; }
        public DbSet<HiringAdvertisement> HiringAdvertisements { get; set; } 
        public DbSet<HiringItemAdvertisement> HiringItemAdvertisements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryEntityConfiguration());
            modelBuilder.ApplyConfiguration(new AdvertisementEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ItemAdvertisementEntityConfiguration());
            modelBuilder.ApplyConfiguration(new HiringAdvertisementEntityConfiguration());
            modelBuilder.ApplyConfiguration(new HiringItemAdvertisementEntityConfiguration());

            modelBuilder.Entity<UserRole>(up => {
                up.HasKey(u => new { u.UserId, u.RoleId });

                up.HasOne(u => u.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(u => u.RoleId)
                .IsRequired();

                up.HasOne(u => u.User)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(u => u.UserId)
                .IsRequired();
            });
        }
    }
}
