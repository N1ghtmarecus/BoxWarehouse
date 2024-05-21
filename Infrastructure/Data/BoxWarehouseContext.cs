using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class BoxWarehouseContext : DbContext
    {
        public BoxWarehouseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Box> Boxes { get; set; }
    }
}
