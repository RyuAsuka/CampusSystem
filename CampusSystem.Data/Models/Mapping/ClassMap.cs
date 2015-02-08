using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CampusSystem.Data.Models.Mapping
{
    public class ClassMap : EntityTypeConfiguration<Class>
    {
        public ClassMap()
        {
            // Primary Key
            this.HasKey(t => t.class_id);

            // Properties
            this.Property(t => t.class_id)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(9);

            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Class");
            this.Property(t => t.class_id).HasColumnName("class_id");
            this.Property(t => t.name).HasColumnName("name");
        }
    }
}
