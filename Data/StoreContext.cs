using Microsoft.EntityFrameworkCore;
using SKINET_Ecommerce.Entities;

namespace SKINET_Ecommerce.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
