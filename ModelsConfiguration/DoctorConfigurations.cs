

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GraduationProject.ModelsConfiguration
{
    public class DoctorConfigurations : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id)
                .UseIdentityColumn();

            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode();

            builder.Property(d => d.Specialization)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.HasIndex(d => d.Email)
                .IsUnique();

            builder.Property(d => d.PhoneNumber)
                .HasMaxLength(20);

            builder.Property(d => d.Country)
                .HasMaxLength(50)
                .IsUnicode();

            builder.Property(d => d.Location)
                .HasMaxLength(200)
                .IsUnicode();

            builder.ToTable("Doctors");
        }
    }
}