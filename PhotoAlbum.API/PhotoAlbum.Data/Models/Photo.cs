using PhotoAlbum.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoAlbum.Data.Models
{
    public class Photo : IBaseEntity, ICreatedTracking, ICreatedUserTracking
    {
        [Key]
        public Guid Id { get; set; }

        public string Filename { get; set; }


        [ForeignKey(nameof(CreatedByUser))]
        public Guid CreatedByUserId { get; set; }
        public virtual ApplicationUser CreatedByUser { get; set; }

        public DateTimeOffset CreatedDate { get; set; }


        public PhotoMetadata Metadata { get; set; }


        [ForeignKey(nameof(Album))]
        public Guid? AlbumId { get; set; }
        public Album Album { get; set; }


        public ICollection<SharedPhoto> SharedWith { get; set; }
    }
}
