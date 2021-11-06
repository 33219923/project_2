using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PhotoAlbum.Data.Models;
using PhotoAlbum.Logic.Interfaces;
using PhotoAlbum.Repository.Interfaces;
using PhotoAlbum.Shared.Constants;
using PhotoAlbum.Shared.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PhotoAlbum.Logic.Implentations
{
    public class UserService : BaseService<User, ApplicationUser>, IUserService
    {
        private readonly ILogger _logger;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(ILogger<UserService> logger, IUserRepository userRepository, IConfiguration configuration) : base(logger, userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public override User Add(User user)
        {
            _logger.LogDebug("Register user called! User:{@user}", user);
            return _userRepository.Add(user);
        }

        public override User Update(User user, Guid userId)
        {
            _logger.LogDebug("Update user called! User: {@user}, UserId: {userId}", user, userId);
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

        public string GetToken(Login login)
        {
            _logger.LogDebug("Get token for user called! Login: {@login}", login);

            var user = _userRepository.ValidateUser(login);

            List<Claim> claimsList = new List<Claim>();
            claimsList.Add(new Claim("userId", user.Id.ToString()));

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration[AppSettings.JWT_SECRET]);

            var jwtSecurityToken = new JwtSecurityToken(
               issuer: _configuration[AppSettings.JWT_ISSUER],
               audience: _configuration[AppSettings.JWT_ISSUER],
               claims: claimsList,
               expires: DateTime.UtcNow.AddHours(double.Parse(_configuration[AppSettings.JWT_EXPIRY_HOURS])),
               signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature));

            var jwtToken = tokenHandler.WriteToken(jwtSecurityToken);

            return jwtToken;
        }
    }
}
