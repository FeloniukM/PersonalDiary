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
                .HasOne(r => r.Author)
                .WithMany(u => u.Records)
                .HasForeignKey(r => r.AuthorId);

            builder.Property(x => x.Title)
                .IsRequired();

            builder
                .Property(x => x.Text)
                .HasMaxLength(500)
                .IsRequired();

            builder
                .HasOne(r => r.Image)
                .WithOne(i => i.Record)
                .HasForeignKey<Image>(r => r.RecordId);

        }
    }
}
