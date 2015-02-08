using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CampusSystem.Data.Models.Mapping
{
    public class GroupMap : EntityTypeConfiguration<Group>
    {
        public GroupMap()
        {
            // Primary Key
            this.HasKey(t => t.group_id);

            // Properties
            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.owner)
                .IsRequired()
                .HasMaxLength(11);

            // Table & Column Mappings
            this.ToTable("Group");
            this.Property(t => t.group_id).HasColumnName("group_id");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.owner).HasColumnName("owner");

            // Relationships
            this.HasMany(t => t.Users)
                .WithMany(t => t.Groups1)
                .Map(m =>
                    {
                        m.ToTable("GroupMember");
                        m.MapLeftKey("group_id");
                        m.MapRightKey("member");
                    });

            this.HasRequired(t => t.User)
                .WithMany(t => t.Groups)
                .HasForeignKey(d => d.owner);

        }
    }
}
