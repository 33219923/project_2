using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Shared.Models
{
    public class PhotoMetadata
    {
        [JsonProperty("photoId")]
        public Guid PhotoId { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; }

        [JsonProperty("geolocation")]
        public string Geolocation { get; set; }
    }
}
