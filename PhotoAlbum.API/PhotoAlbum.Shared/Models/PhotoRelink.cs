using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Shared.Models
{
    public class PhotoRelink
    {
        [JsonProperty("albumId")]
        public Guid AlbumId { get; set; }

        [JsonProperty("photoIds")]
        public List<Guid> PhotoIds { get; set; } = new List<Guid>();
    }
}
