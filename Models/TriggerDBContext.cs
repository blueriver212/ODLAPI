using Microsoft.EntityFrameworkCore;

namespace ODLAPI.Models
{
    public class TriggerDBContext : DbContext
    {
        public TriggerDBContext(DbContextOptions<TriggerDBContext> options)
            :base(options)
        {

        }

        // Representation of the data in the database 
        public DbSet<Trigger> Triggers { get; set; } = null;
    }

}
