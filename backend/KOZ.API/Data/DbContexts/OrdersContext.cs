using KOZ.API.Data.DataClasses;
using Microsoft.EntityFrameworkCore;

namespace KOZ.API.Data.DbContexts
{
    public class OrdersContext : DbContext
    {
        public OrdersContext(DbContextOptions<OrdersContext> options)
            : base(options) {}

        public DbSet<Order> Orders { get; set; }
        public DbSet<Worker> Workers { get; set; }
    }
}
