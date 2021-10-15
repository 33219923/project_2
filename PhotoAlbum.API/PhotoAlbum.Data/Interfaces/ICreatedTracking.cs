using PhotoAlbum.Data.Models;
using System;

namespace PhotoAlbum.Data.Interfaces
{
    public interface ICreatedTracking
    {
        DateTimeOffset CreatedDate { get; set; }
    }
}
