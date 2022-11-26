using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonalDiary.DAL.DataAccess;
using PersonalDiary.DAL.Interfaces;

namespace PersonalDiary.DAL
{
    public static class Setup
    {
        public static void AddRepository(this IServiceCollection service)
        {
            service.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            if (connectionString == null)
                throw new ArgumentNullException(nameof(connectionString));

            services.AddDbContext<PersonalDiaryDbContext>(x => x.UseSqlServer(connectionString));
            
        }
    }
}
