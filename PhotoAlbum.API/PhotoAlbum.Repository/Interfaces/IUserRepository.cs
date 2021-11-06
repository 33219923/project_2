using PhotoAlbum.Repository.Interfaces.Base;
using PhotoAlbum.Shared.Models;
using System;
using UserDto = PhotoAlbum.Shared.Models.User;
using UserModel = PhotoAlbum.Data.Models.ApplicationUser;

namespace PhotoAlbum.Repository.Interfaces
{
    public interface IUserRepository : ISearchableRepository<UserDto, UserModel>
    {
        void ChangePassword(Guid userId, string currentPassword, string newPassword);
        UserDto ValidateUser(Login login);
    }
}
