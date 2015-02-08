using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CampusSystem.Data.Models.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.user_id);

            // Properties
            this.Property(t => t.user_id)
                .IsRequired()
                .HasMaxLength(11);

            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.password)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.class_id)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(9);

            this.Property(t => t.role)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("User");
            this.Property(t => t.user_id).HasColumnName("user_id");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.password).HasColumnName("password");
            this.Property(t => t.class_id).HasColumnName("class_id");
            this.Property(t => t.role).HasColumnName("role");

            // Relationships
            this.HasRequired(t => t.Class)
                .WithMany(t => t.Users)
                .HasForeignKey(d => d.class_id);

        }
    }
}
