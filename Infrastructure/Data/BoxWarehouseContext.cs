using Application.Services;
using Domain.Common;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Data
{
    public class BoxWarehouseContext : IdentityDbContext<ApplicationUser>
    {
        private readonly UserResolverService _userService;
        public BoxWarehouseContext(DbContextOptions<BoxWarehouseContext> options, UserResolverService userService) : base(options)
        {
            _userService = userService;
        }

        public DbSet<Box> Boxes { get; set; }
        public DbSet<Picture> Pictures { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is AuditableEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((AuditableEntity)entityEntry.Entity).LastModified = DateTime.Now;
                ((AuditableEntity)entityEntry.Entity).LastModifiedBy = _userService.GetUser();

                if (entityEntry.State == EntityState.Added)
                {
                    ((AuditableEntity)entityEntry.Entity).Created = DateTime.Now;
                    ((AuditableEntity)entityEntry.Entity).CreatedBy = _userService.GetUser();
                }
            }

                return await base.SaveChangesAsync();
        }
    }
}
