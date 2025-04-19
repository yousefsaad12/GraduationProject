using GraduationProject.ModelsConfiguration;

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
            builder.ApplyConfiguration(new DoctorConfigurations());
            base.OnModelCreating(builder);
        }
    }
}