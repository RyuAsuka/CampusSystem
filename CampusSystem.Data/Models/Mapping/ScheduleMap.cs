using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CampusSystem.Data.Models.Mapping
{
    public class ScheduleMap : EntityTypeConfiguration<Schedule>
    {
        public ScheduleMap()
        {
            // Primary Key
            this.HasKey(t => new { t.student_id, t.course_id });

            // Properties
            this.Property(t => t.student_id)
                .IsRequired()
                .HasMaxLength(11);

            this.Property(t => t.course_id)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Schedule");
            this.Property(t => t.student_id).HasColumnName("student_id");
            this.Property(t => t.course_id).HasColumnName("course_id");
            this.Property(t => t.score).HasColumnName("score");

            // Relationships
            this.HasRequired(t => t.Course)
                .WithMany(t => t.Schedules)
                .HasForeignKey(d => d.course_id);
            this.HasRequired(t => t.User)
                .WithMany(t => t.Schedules)
                .HasForeignKey(d => d.student_id);

        }
    }
}
