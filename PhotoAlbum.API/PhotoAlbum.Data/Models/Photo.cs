using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Data.Models
{
    public class Photo
    {
        public NpgsqlTsVector SearchVector { get; set; }
    }
}
