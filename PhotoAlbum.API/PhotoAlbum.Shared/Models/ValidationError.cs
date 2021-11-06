using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace PhotoAlbum.Shared.Models
{
    public class ValidationError : ErrorDetails
    {
        [JsonProperty("fieldErrors")]
        public Dictionary<string, string> FieldErrors { get; set; } = new Dictionary<string, string>();
    }
}
