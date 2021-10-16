using Microsoft.Extensions.DependencyInjection;
using PhotoAlbum.Repository.Implementations;
using PhotoAlbum.Repository.Interfaces;

namespace PhotoAlbum.Repository.Utils
{
    public static class RepositoryConfig
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IAlbumRepository, AlbumRepository>();
            services.AddSingleton<IPhotoRepository, PhotoRepository>();
            services.AddSingleton<IPhotoMetadataRepository, PhotoMetadataRepository>();
            services.AddSingleton<ISharedAlbumRepository, SharedAlbumRepository>();
            services.AddSingleton<ISharedPhotoRepository, SharedPhotoRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
        }
    }
}
