using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DataEncryption;
using Microsoft.EntityFrameworkCore.DataEncryption.Providers;
using PersonalDiary.DAL.Configuration;
using PersonalDiary.DAL.Entities;

namespace PersonalDiary.DAL.DataAccess
{
    public class PersonalDiaryDbContext : DbContext
    {
        private readonly byte[] _encryptionKey = AesProvider.GenerateKey(AesKeySize.AES128Bits).Key;
        private readonly byte[] _encryptionIV = AesProvider.GenerateKey(AesKeySize.AES128Bits).IV;
        private readonly IEncryptionProvider _provider;

        public PersonalDiaryDbContext(DbContextOptions<PersonalDiaryDbContext> options) : base(options)
        {
            _provider = new AesProvider(
                _encryptionKey,
                _encryptionIV,
                System.Security.Cryptography.CipherMode.CBC,
                System.Security.Cryptography.PaddingMode.Zeros);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.UseEncryption(_provider);
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RecordConfiguration());
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Record> Records { get; set; } = null!;
        public DbSet<Image> Images { get; set; } = null!;
        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
    }
}
