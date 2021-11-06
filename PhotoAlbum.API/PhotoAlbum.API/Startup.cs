using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PhotoAlbum.Data;
using PhotoAlbum.Data.Models;
using PhotoAlbum.Logic.Utils;
using PhotoAlbum.Repository.Utils;
using PhotoAlbum.Shared.Constants;
using PhotoAlbum.Shared.Middleware;
using PhotoAlbum.Shared.Services;

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

            services.AddScoped<IRequestState, RequestState>();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(Configuration[AppSettings.DB_CONNECTION], x =>
                {
                    x.MigrationsAssembly("PhotoAlbum.Data");
                });
            });

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAutoMapConfig();
            services.AddLogicServices();
            services.AddRepositories();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Temporarily expose swagger endpoint in deployment
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PhotoAlbum.API v1"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PhotoAlbum.API v1"));
            }

            //Running automatic migration
            using (var scope = app.ApplicationServices.CreateScope())
            using (var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
                db.Database.Migrate();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<RequestStateMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
