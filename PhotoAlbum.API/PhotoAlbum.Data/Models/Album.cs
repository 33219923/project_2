using PhotoAlbum.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoAlbum.Data.Models
{
    public class Album : IBaseEntity, ICreatedTracking, ICreatedUserTracking
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }


        [ForeignKey(nameof(CreatedByUser))]
        public Guid CreatedByUserId { get; set; }
        public virtual ApplicationUser CreatedByUser { get; set; }

        public DateTimeOffset CreatedDate { get; set; }


        public ICollection<Photo> Photos { get; set; }
        public ICollection<SharedAlbum> SharedWith { get; set; }
    }
}
