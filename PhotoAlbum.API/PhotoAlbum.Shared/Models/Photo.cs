using Newtonsoft.Json;
using System;

namespace PhotoAlbum.Shared.Models
{
    public class Photo
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("filename")]
        public string Filename { get; set; }

        [JsonProperty("createdByUserId")]
        public Guid CreatedByUserId { get; set; }

        [JsonProperty("createdDate")]
        public DateTimeOffset? CreatedDate { get; set; }

        [JsonProperty("metadata")]
        public PhotoMetadata Metadata { get; set; }

        [JsonProperty("data")]
        public byte[] Data { get; set; }
    }
}
