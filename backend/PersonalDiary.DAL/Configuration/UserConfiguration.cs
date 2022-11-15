using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalDiary.DAL.Entities;

namespace PersonalDiary.DAL.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Nickname)
                .HasMaxLength(30)
                .IsRequired();

            builder
                .Property(x => x.Password)
                .IsRequired();

            builder
                .Property(x => x.Email)
                .HasMaxLength(50)
                .IsRequired();

        }
    }
}
