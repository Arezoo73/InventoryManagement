using InventoryManagement.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Data.DatabaseContexts
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer("Server=.;Database=InventoryManagement;Integrated Security=True;TrustServerCertificate=True;");
            }
        }
        public DbSet<InventoryItem> Items { get; set; }

    }
}
