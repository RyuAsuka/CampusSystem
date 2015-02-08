using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CampusSystem.Data.Models.Mapping
{
    public class ShuttleMap : EntityTypeConfiguration<Shuttle>
    {
        public ShuttleMap()
        {
            // Primary Key
            this.HasKey(t => t.shuttle_id);

            // Properties
            this.Property(t => t.start_location)
                .HasMaxLength(10);

            this.Property(t => t.end_location)
                .HasMaxLength(10);

            this.Property(t => t.weekday)
                .HasMaxLength(14);

            // Table & Column Mappings
            this.ToTable("Shuttle");
            this.Property(t => t.shuttle_id).HasColumnName("shuttle_id");
            this.Property(t => t.time).HasColumnName("time");
            this.Property(t => t.start_location).HasColumnName("start_location");
            this.Property(t => t.end_location).HasColumnName("end_location");
            this.Property(t => t.weekday).HasColumnName("weekday");
        }
    }
}
