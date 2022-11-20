using Microsoft.EntityFrameworkCore;
using PersonalDiary.DAL.Entities;

namespace PersonalDiary.DAL.DataSeed
{
    public static class Seed
    {
        public static void Seeding(this ModelBuilder builder)
        {
            SeedBaseUser(builder);
        }

        public static void SeedBaseUser(ModelBuilder builder) =>
            builder.Entity<User>().HasData(
                new User()
                {
                    Id = Guid.NewGuid(),
                    Nickname = "admin",
                    Email = "tester@gmail.com",
                    Password = "Password_1",
                    CreatedAt = DateTime.UtcNow,
                    IsAdmin = true,
                    IsDelete = false,
                    Salt = "D;%yL9TS:5PalS/d",
                    Records = null
                });
    }
}
