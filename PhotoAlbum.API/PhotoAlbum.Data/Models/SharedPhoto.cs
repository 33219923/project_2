using PhotoAlbum.Data.Interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoAlbum.Data.Models
{
    public class SharedPhoto: ICreatedTracking
    {
        public DateTimeOffset CreatedDate { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }


        [ForeignKey(nameof(Photo))]
        public Guid PhotoId { get; set; }
        public Photo Photo { get; set; }
    }
}
