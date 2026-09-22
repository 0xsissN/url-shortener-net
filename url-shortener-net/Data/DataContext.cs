using Microsoft.EntityFrameworkCore;
using url_shortener_net.Entities;

namespace url_shortener_net.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Url> Url => Set<Url>();
    }
}
