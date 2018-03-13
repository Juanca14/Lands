using System.Data.Entity;

namespace Lands.Domain
{
    public class DBContext : DbContext
    {
        public DBContext() : base("DefaultConnection")
        {
                
        }

        public System.Data.Entity.DbSet<Lands.Domain.User> Users { get; set; }
    }
}
