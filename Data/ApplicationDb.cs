

namespace GraduationProject.Data
{
    public class ApplicationDb : IdentityDbContext<User>
    {
        public ApplicationDb(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Doctor> Doctors { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder.Entity<Doctor>(entity =>
            {

                entity.ToTable("Doctors");
            });
        }
    }
}