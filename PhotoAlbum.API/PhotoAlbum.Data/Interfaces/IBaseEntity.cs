using System;

namespace PhotoAlbum.Data.Interfaces
{
    //This is the base entity interface for the data models
    public interface IBaseEntity
    {
        Guid Id { get; set; }
    }
}
