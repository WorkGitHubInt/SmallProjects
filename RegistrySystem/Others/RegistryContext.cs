using System.Data.Entity;

namespace RegistrySystem
{
    public class RegistryContext : DbContext
    {
        public RegistryContext() : base("DbConnection") { }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
