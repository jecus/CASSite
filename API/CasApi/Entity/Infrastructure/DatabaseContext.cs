using Entity.Entity.General;
using Microsoft.EntityFrameworkCore;

namespace Entity.Infrastructure
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions opt)
            : base(opt)
        {

        }
        
        public DbSet<Aircraft> Aircrafts { get; set; }
    }
}