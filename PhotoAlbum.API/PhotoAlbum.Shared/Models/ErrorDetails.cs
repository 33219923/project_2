using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Shared.Models
{
    public class ErrorDetails
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

#if DEBUG
        [JsonProperty("debug")]
        public object Debug { get; set; }
#endif

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
