using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CampusSystem.Data.Models.Mapping
{
    public class MessageMap : EntityTypeConfiguration<Message>
    {
        public MessageMap()
        {
            // Primary Key
            this.HasKey(t => t.message_id);

            // Properties
            this.Property(t => t.sender)
                .IsRequired()
                .HasMaxLength(11);

            this.Property(t => t.message_content)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Message");
            this.Property(t => t.message_id).HasColumnName("message_id");
            this.Property(t => t.sender).HasColumnName("sender");
            this.Property(t => t.send_time).HasColumnName("send_time");
            this.Property(t => t.message_content).HasColumnName("message_content");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.Messages)
                .HasForeignKey(d => d.sender);

        }
    }
}
