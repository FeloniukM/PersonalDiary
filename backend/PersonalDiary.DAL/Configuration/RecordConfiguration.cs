using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PersonalDiary.DAL.Entities;

namespace PersonalDiary.DAL.Configuration
{
    internal class RecordConfiguration : IEntityTypeConfiguration<Record>
    {
        public void Configure(EntityTypeBuilder<Record> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .HasOne(x => x.Author)
                .WithMany(x => x.Records)
                .HasForeignKey(x => x.AuthorId);

            builder.Property(x => x.Title)
                .IsRequired();

            builder
                .Property(x => x.Text)
                .HasMaxLength(500)
                .IsRequired();

        }
    }
}
