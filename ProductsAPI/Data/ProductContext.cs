using Microsoft.EntityFrameworkCore;

namespace ProductsAPI.Data
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProductContext(DbContextOptions<ProductContext> opts)
            : base(opts)
        {
        }
    }
}
