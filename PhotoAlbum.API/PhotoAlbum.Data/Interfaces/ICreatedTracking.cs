using PhotoAlbum.Data.Models;
using System;

namespace PhotoAlbum.Data.Interfaces
{
    //This is the interface for tracking the created date on database models
    public interface ICreatedTracking
    {
        DateTimeOffset CreatedDate { get; set; }
    }
}
