using PhotoAlbum.Repository.Interfaces.Base;

using UserDto = PhotoAlbum.Shared.Models.User;
using UserModel = PhotoAlbum.Data.Models.ApplicationUser;

namespace PhotoAlbum.Repository.Interfaces
{
    public interface IUserRepository : ISearchableRepository<UserDto, UserModel>
    {
    }
}
