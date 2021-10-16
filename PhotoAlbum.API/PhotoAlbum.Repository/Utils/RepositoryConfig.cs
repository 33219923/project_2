using Microsoft.Extensions.DependencyInjection;
using PhotoAlbum.Repository.Implementations;
using PhotoAlbum.Repository.Interfaces;

namespace PhotoAlbum.Repository.Utils
{
    public static class RepositoryConfig
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAlbumRepository, AlbumRepository>();
            services.AddScoped<IPhotoRepository, PhotoRepository>();
            services.AddScoped<IPhotoMetadataRepository, PhotoMetadataRepository>();
            services.AddScoped<ISharedAlbumRepository, SharedAlbumRepository>();
            services.AddScoped<ISharedPhotoRepository, SharedPhotoRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
