using Microsoft.Extensions.Logging;
using PhotoAlbum.Data.Models;
using PhotoAlbum.Logic.Interfaces;
using PhotoAlbum.Repository.Interfaces;
using PhotoAlbum.Shared.Models;
using System;

namespace PhotoAlbum.Logic.Implentations
{
    public class UserService : BaseService<User, ApplicationUser>, IUserService
    {
        private readonly ILogger _logger;
        private readonly IUserRepository _userRepository;

        public UserService(ILogger<UserService> logger, IUserRepository userRepository) : base(logger, userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public override User Add(User user)
        {
            _logger.LogDebug("Register user called! User:{user}", user);
            return _userRepository.Add(user);
        }

        public override User Update(User user, Guid userId)
        {
            _logger.LogDebug("Update user called! User: {user}, UserId: {userId}", user, userId);
            return _userRepository.Update(user, userId);
        }

        public override void Delete(Guid userId)
        {
            _logger.LogDebug("Delete user called! UserId: {userId}", userId);
            _userRepository.Delete(userId);
        }

        public void ChangePassword(Guid userId, string currentPassword, string newPassword)
        {
            _logger.LogDebug("Change password for user called! UserId: {userId}, CurrentPassword: {currentPassword}, NewPassword: {newPassword}", userId, currentPassword, newPassword);
            _userRepository.ChangePassword(userId, currentPassword, newPassword);
        }
    }
}
