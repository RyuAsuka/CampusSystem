using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CampusSystem.Data.Models.Mapping
{
    public class LendMap : EntityTypeConfiguration<Lend>
    {
        public LendMap()
        {
            // Primary Key
            this.HasKey(t => t.lend_id);

            // Properties
            this.Property(t => t.user_id)
                .IsRequired()
                .HasMaxLength(11);

            this.Property(t => t.copy_id)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Lend");
            this.Property(t => t.lend_id).HasColumnName("lend_id");
            this.Property(t => t.user_id).HasColumnName("user_id");
            this.Property(t => t.copy_id).HasColumnName("copy_id");
            this.Property(t => t.lend_time).HasColumnName("lend_time");
            this.Property(t => t.expire_time).HasColumnName("expire_time");
            this.Property(t => t.return_time).HasColumnName("return_time");

            // Relationships
            this.HasRequired(t => t.Copy)
                .WithMany(t => t.Lends)
                .HasForeignKey(d => d.copy_id);
            this.HasRequired(t => t.User)
                .WithMany(t => t.Lends)
                .HasForeignKey(d => d.user_id);

        }
    }
}
