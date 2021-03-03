using Microsoft.EntityFrameworkCore;
using UVS.Model;

namespace UVS.EF
{
    class Context: DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            
        }

        public DbSet<Row> Row { get; set; }
    }
}
