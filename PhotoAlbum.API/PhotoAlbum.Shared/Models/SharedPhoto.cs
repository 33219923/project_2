using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Shared.Models
{
    public class SharedPhoto
    {
        [JsonProperty("photoId")]
        public Guid PhotoId { get; set; }

        [JsonProperty("userId")]
        public Guid UserId { get; set; }
    }
}
