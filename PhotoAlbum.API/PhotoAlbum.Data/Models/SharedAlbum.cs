using PhotoAlbum.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Data.Models
{
    public class SharedAlbum : ICreatedTracking
    {
        public DateTimeOffset CreatedDate { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }


        [ForeignKey(nameof(Album))]
        public Guid AlbumId { get; set; }
        public Album Album { get; set; }
    }
}
