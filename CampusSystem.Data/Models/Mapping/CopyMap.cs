using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CampusSystem.Data.Models.Mapping
{
    public class CopyMap : EntityTypeConfiguration<Copy>
    {
        public CopyMap()
        {
            // Primary Key
            this.HasKey(t => t.copy_id);

            // Properties
            this.Property(t => t.copy_id)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.isbn)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Copy");
            this.Property(t => t.copy_id).HasColumnName("copy_id");
            this.Property(t => t.isbn).HasColumnName("isbn");
            this.Property(t => t.status).HasColumnName("status");

            // Relationships
            this.HasRequired(t => t.Book)
                .WithMany(t => t.Copies)
                .HasForeignKey(d => d.isbn);

        }
    }
}
