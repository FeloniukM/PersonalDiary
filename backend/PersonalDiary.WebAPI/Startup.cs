using PersonalDiary.BLL;
using PersonalDiary.BLL.Service;
using PersonalDiary.Common.DTO.Image;
using PersonalDiary.Common.Email;
using PersonalDiary.WebAPI.Extensions;

namespace PersonalDiary.WebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        private readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddAuthorization();
            services.AddCustomService(Configuration);
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.Configure<ImageStorageOptions>(Configuration.GetSection("ImageStorageOptions"));
            services.AddMemoryCache();
            services.AddDistributedSqlServerCache(options =>
            {
                options.ConnectionString = Configuration.GetConnectionString("DefaultConnection");
                options.SchemaName = "dbo";
                options.TableName = "SqlCache";
            });
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            });
            services.AddHostedService<TimedHostedService>();
            services.ConfigureJwt(Configuration);
            services.AddAutoMapper();
            services.AddFluentValidation();
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins, policy =>
                {
                    policy.WithOrigins("https://localhost:4200", "http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
            services.AddEndpointsApiExplorer();
            services.AddSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();


            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
