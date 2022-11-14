using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonalDiary.DAL;

namespace PersonalDiary.BLL
{
    public static class Setup
    {
        public static void AddCustomService(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext(configuration);
            service.AddRepository();
        }
    }
}