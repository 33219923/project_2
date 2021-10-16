using Microsoft.Extensions.DependencyInjection;
using PhotoAlbum.Logic.Implentations;
using PhotoAlbum.Logic.Interfaces;

namespace PhotoAlbum.Logic.Utils
{
    public static class LogicConfig
    {
        public static void AddLogicServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAlbumService, AlbumService>();
            services.AddScoped<IPhotoService, PhotoService>();
        }
    }
}
