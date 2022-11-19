using PersonalDiary.BLL;
using PersonalDiary.Common.Email;
using PersonalDiary.WebAPI.Extensions;

namespace PersonalDiary.WebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

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
            services.ConfigureJwt(Configuration);
            services.AddAutoMapper();
            services.AddFluentValidation();
            services.AddCors();
            services.AddEndpointsApiExplorer();
            services.AddSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseCors(builder =>
                builder
                    .AllowAnyHeader()
                    .AllowAnyOrigin()
                    .AllowAnyMethod());

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
