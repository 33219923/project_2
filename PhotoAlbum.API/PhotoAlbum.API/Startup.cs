using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PhotoAlbum.Data;
using PhotoAlbum.Repository.Utils;
using PhotoAlbum.Shared.Constants;

namespace PhotoAlbum.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PhotoAlbum.API", Version = "v1" });
            });

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(Configuration[AppSettings.DB_CONNECTION], x =>
                {
                    x.MigrationsAssembly("PhotoAlbum.Data");
                });
            });

            services.AddAutoMapConfig();
            services.AddRepositories();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            ApplicationDbContext db,
            ILoggerFactory loggerFactory
            )
        {

            var logger = loggerFactory.CreateLogger<Startup>();
            //logger?.LogInformation("Configuration: {configuration}", JsonConvert.SerializeObject(Configuration.AsEnumerable().ToList(), Formatting.Indented));
            logger?.LogInformation("ConnectionString: {c}", Configuration[AppSettings.DB_CONNECTION]);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PhotoAlbum.API v1"));
            }

            //Running automatic migration
            db.Database.Migrate();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
