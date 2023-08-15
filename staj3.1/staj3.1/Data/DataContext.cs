using Microsoft.EntityFrameworkCore;

namespace staj3._1.Data
{
    public class DataContext : DbContext
    {
        public  DataContext (DbContextOptions<DataContext> options) : base (options) { }

        public DbSet<Location>Location4 { get; set; }   
    }
}
