using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CampusSystem.Data.Models.Mapping
{
    public class CourseMap : EntityTypeConfiguration<Course>
    {
        public CourseMap()
        {
            // Primary Key
            this.HasKey(t => t.course_id);

            // Properties
            this.Property(t => t.course_id)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.teacher_id)
                .IsRequired()
                .HasMaxLength(11);

            this.Property(t => t.time)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.place)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Course");
            this.Property(t => t.course_id).HasColumnName("course_id");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.teacher_id).HasColumnName("teacher_id");
            this.Property(t => t.time).HasColumnName("time");
            this.Property(t => t.credits).HasColumnName("credits");
            this.Property(t => t.exam_time).HasColumnName("exam_time");
            this.Property(t => t.place).HasColumnName("place");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.Courses)
                .HasForeignKey(d => d.teacher_id);

        }
    }
}
