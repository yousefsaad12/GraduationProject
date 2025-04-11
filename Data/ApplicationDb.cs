
namespace GraduationProject.Data
{
    public class ApplicationDb : IdentityDbContext<User>
    {
        public ApplicationDb(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users;
        public DbSet<Doctor> Doctors;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}