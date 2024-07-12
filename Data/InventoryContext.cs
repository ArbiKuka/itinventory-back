using Microsoft.EntityFrameworkCore;
using ITInventoryAPI.Models;

namespace ITInventoryAPI.Data
{
    public class InventoryContext : DbContext
    {
        public InventoryContext(DbContextOptions<InventoryContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Device> Devices { get; set; }
    }
}