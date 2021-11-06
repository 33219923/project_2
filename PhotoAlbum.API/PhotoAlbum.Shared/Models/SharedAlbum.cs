using Newtonsoft.Json;
using System;

namespace PhotoAlbum.Shared.Models
{
    public class SharedAlbum
    {
        [JsonProperty("albumId")]
        public Guid AlbumId { get; set; }

        [JsonProperty("userId")]
        public Guid UserId { get; set; }
    }
}
