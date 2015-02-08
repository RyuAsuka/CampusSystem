using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CampusSystem.Data.Models.Mapping
{
    public class SendToMap : EntityTypeConfiguration<SendTo>
    {
        public SendToMap()
        {
            // Primary Key
            this.HasKey(t => new { t.message_id, t.receiver });

            // Properties
            this.Property(t => t.message_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.receiver)
                .IsRequired()
                .HasMaxLength(11);

            // Table & Column Mappings
            this.ToTable("SendTo");
            this.Property(t => t.message_id).HasColumnName("message_id");
            this.Property(t => t.receiver).HasColumnName("receiver");
            this.Property(t => t.is_sent).HasColumnName("is_sent");

            // Relationships
            this.HasRequired(t => t.Message)
                .WithMany(t => t.SendToes)
                .HasForeignKey(d => d.message_id);
            this.HasRequired(t => t.User)
                .WithMany(t => t.SendToes)
                .HasForeignKey(d => d.receiver);

        }
    }
}
