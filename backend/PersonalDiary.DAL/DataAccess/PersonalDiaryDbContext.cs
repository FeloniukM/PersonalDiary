using Microsoft.EntityFrameworkCore;
using PersonalDiary.DAL.Configuration;
using PersonalDiary.DAL.DataSeed;
using PersonalDiary.DAL.Entities;

namespace PersonalDiary.DAL.DataAccess
{
    public class PersonalDiaryDbContext : DbContext
    {
        public PersonalDiaryDbContext(DbContextOptions<PersonalDiaryDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RecordConfiguration());
            modelBuilder.Seeding();
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Record> Records { get; set; } = null!;
    }
}
