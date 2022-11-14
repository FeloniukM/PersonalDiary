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
                    Id = new Guid(),
                    Nickname = "admin",
                    Email = "tester@gmail.com",
                    Password = "Password_1",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsAdmin = true,
                    IsDelete = false,
                    Records = null
                });
    }
}
