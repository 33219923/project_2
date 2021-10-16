using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Repository.Utils
{
    //AutoMapper configuration and setup
    public static class AutoMapper
    {
        public static void AddAutoMapConfig(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.CreateMap<Shared.Models.Album, Data.Models.Album>();
                cfg.CreateMap<Data.Models.Album, Shared.Models.Album>();

                cfg.CreateMap<Shared.Models.Photo, Data.Models.Photo>();
                cfg.CreateMap<Data.Models.Photo, Shared.Models.Photo>();

                cfg.CreateMap<Shared.Models.PhotoMetadata, Data.Models.PhotoMetadata>();
                cfg.CreateMap<Data.Models.PhotoMetadata, Shared.Models.PhotoMetadata>();

                cfg.CreateMap<Shared.Models.SharedAlbum, Data.Models.SharedAlbum>();
                cfg.CreateMap<Data.Models.SharedAlbum, Shared.Models.SharedAlbum>();

                cfg.CreateMap<Shared.Models.SharedPhoto, Data.Models.SharedPhoto>();
                cfg.CreateMap<Data.Models.SharedPhoto, Shared.Models.SharedPhoto>();

                cfg.CreateMap<Shared.Models.User, Data.Models.ApplicationUser>();
                cfg.CreateMap<Data.Models.ApplicationUser, Shared.Models.User>();
            },
            //Provide context where models exist
            typeof(Shared.Models.User), typeof(Data.Models.ApplicationUser));
        }
    }
}
