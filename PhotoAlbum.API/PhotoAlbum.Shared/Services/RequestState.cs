using System;

namespace PhotoAlbum.Shared.Services
{
    public interface IRequestState
    {
        Guid? UserId { get; set; }
    }

    public class RequestState : IRequestState
    {
        public Guid? UserId { get; set; }
    }
}
