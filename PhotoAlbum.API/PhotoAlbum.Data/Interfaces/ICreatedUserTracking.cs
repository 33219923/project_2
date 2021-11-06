using PhotoAlbum.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Data.Interfaces
{
    public interface ICreatedUserTracking
    {
        Guid CreatedByUserId { get; set; }
    }
}
