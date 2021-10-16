using NpgsqlTypes;
using PhotoAlbum.Data.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoAlbum.Data.Models
{
    public class PhotoMetadata : ICreatedTracking, ISearchable
    {
        [Key]
        [ForeignKey(nameof(Photo))]
        public Guid PhotoId { get; set; }
        public Photo Photo { get; set; }

        public string Tags { get; set; }
        public string Geolocation { get; set; }



        [ForeignKey(nameof(CreatedByUser))]
        public Guid CreatedByUserId { get; set; }
        public virtual ApplicationUser CreatedByUser { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public NpgsqlTsVector SearchVector { get; set; }

    }
}
