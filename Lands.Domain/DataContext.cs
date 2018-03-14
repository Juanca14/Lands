
namespace Lands.Domain
{
    using System.Data.Entity;

    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<UserType> UserTypes { get; set; }

        public DataContext() : base("DefaultConnection")
        {

        }
    }
}
