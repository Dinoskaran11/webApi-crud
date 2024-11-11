using Crud_webApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace Crud_webApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<WebApi> SuperHeroes { get; set; }
    }
}
