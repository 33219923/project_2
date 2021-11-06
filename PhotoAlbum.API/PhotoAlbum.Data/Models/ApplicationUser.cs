using Microsoft.AspNetCore.Identity;
using NpgsqlTypes;
using PhotoAlbum.Data.Interfaces;
using System;
using System.Collections.Generic;

namespace PhotoAlbum.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>, IBaseEntity, ICreatedTracking, ISearchable
    {
        public ApplicationUser() : base()
        {
            this.Id = Guid.NewGuid();
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

        public NpgsqlTsVector SearchVector { get; set; }


        public ICollection<Album> Albums { get; set; }
        public ICollection<Photo> Photos { get; set; }

        public ICollection<SharedAlbum> SharedAlbums { get; set; }
        public ICollection<SharedPhoto> SharedPhotos { get; set; }
    }
}
