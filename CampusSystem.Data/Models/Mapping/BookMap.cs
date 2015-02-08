using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CampusSystem.Data.Models.Mapping
{
    public class BookMap : EntityTypeConfiguration<Book>
    {
        public BookMap()
        {
            // Primary Key
            this.HasKey(t => t.isbn);

            // Properties
            this.Property(t => t.isbn)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.name)
                .IsRequired();

            this.Property(t => t.book_index)
                .IsRequired()
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("Book");
            this.Property(t => t.isbn).HasColumnName("isbn");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.book_index).HasColumnName("book_index");
        }
    }
}
