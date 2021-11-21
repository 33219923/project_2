using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Shared.Models
{
    public class Album
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("createdByUserId")]
        public Guid CreatedByUserId { get; set; }

        [JsonProperty("previewFilename")]
        public string PreviewFilename { get; set; }

        [JsonProperty("previewData")]
        public byte[] PreviewData { get; set; }
    }
}
