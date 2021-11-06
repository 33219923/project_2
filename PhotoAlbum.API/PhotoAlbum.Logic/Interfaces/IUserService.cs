using PhotoAlbum.Logic.Interfaces.Base;
using PhotoAlbum.Shared.Models;
using System;

namespace PhotoAlbum.Logic.Interfaces
{
    public interface IUserService : IBaseService<User>
    {
        void ChangePassword(Guid userId, string currentPassword, string newPassword);
        string GetToken(Login login);
    }
}
