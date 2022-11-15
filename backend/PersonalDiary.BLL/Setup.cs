﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonalDiary.BLL.Interfaces;
using PersonalDiary.BLL.JWT;
using PersonalDiary.BLL.MappingProfiles;
using PersonalDiary.BLL.Service;
using PersonalDiary.DAL;
using System.Reflection;

namespace PersonalDiary.BLL
{
    public static class Setup
    {
        public static void AddCustomService(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IAuthService, AuthService>();
            service.AddScoped<IJwtFactory, JwtFactory>();

            service.AddDbContext(configuration);
            service.AddRepository();
        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<UserProfile>();
            },
            Assembly.GetExecutingAssembly());
        }
    }
}