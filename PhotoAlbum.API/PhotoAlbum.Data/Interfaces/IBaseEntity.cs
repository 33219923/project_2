using System;

namespace PhotoAlbum.Data.Interfaces
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }
    }
}
