using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DataEncryption;
using Microsoft.EntityFrameworkCore.DataEncryption.Providers;
using Microsoft.Extensions.Options;
using PersonalDiary.DAL.Configuration;
using PersonalDiary.DAL.Encryptions;
using PersonalDiary.DAL.Entities;

namespace PersonalDiary.DAL.DataAccess
{
    public class PersonalDiaryDbContext : DbContext
    {
        private readonly byte[] _encryptionKey = new byte[16] { 123, 12, 124, 15, 14, 123, 46, 146, 252, 12, 67, 23, 53, 13, 14, 15 };
        private readonly byte[] _encryptionIV = new byte[16] { 124, 12, 123, 15, 14, 123, 46, 146, 252, 12, 67, 23, 53, 13, 14, 15 };
        //private readonly EncryptionOptions _encryptionOptions;
        private readonly IEncryptionProvider _provider;

        public PersonalDiaryDbContext(DbContextOptions<PersonalDiaryDbContext> options) : base(options)
        {
            //_encryptionOptions = encOptions.Value;

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
