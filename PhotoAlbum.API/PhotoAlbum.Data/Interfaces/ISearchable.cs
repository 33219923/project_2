using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Data.Interfaces
{
    public interface ISearchable
    {
        NpgsqlTsVector SearchVector { get; set; }
    }
}
