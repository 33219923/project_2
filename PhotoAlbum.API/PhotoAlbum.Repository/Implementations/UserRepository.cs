using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PhotoAlbum.Data;
using PhotoAlbum.Repository.Implementations.Base;
using PhotoAlbum.Repository.Interfaces;
using PhotoAlbum.Shared.Exceptions;
using System;
using System.Linq;
using UserDto = PhotoAlbum.Shared.Models.User;
using UserModel = PhotoAlbum.Data.Models.ApplicationUser;

namespace PhotoAlbum.Repository.Implementations
{
    public class UserRepository : SearchableRepository<UserDto, UserModel>, IUserRepository
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly ApplicationDbContext _db;
        private readonly IMapper _autoMapper;
        private readonly ILogger _logger;

        public UserRepository(UserManager<UserModel> userManager, ApplicationDbContext db, ILogger<UserRepository> logger, IMapper autoMapper) : base(db, logger, autoMapper)
        {
            _userManager = userManager;
            _db = db;
            _logger = logger;
            _autoMapper = autoMapper;
        }

        public override UserDto Add(UserDto dto)
        {
            try
            {
                var user = _autoMapper.Map<UserModel>(dto);
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, dto.Password);

                var result = _userManager.CreateAsync(user).Result;

                if (!result.Succeeded) throw new Exception(string.Join(" ", result.Errors.Select(x => x.Description)));

                return _autoMapper.Map<UserDto>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to add the user. Error: {ex}", ex);
                throw;
            }
        }

        public override UserDto Update(UserDto dto, Guid id)
        {
            try
            {
                var user = _userManager.FindByIdAsync(id.ToString()).Result;
                _autoMapper.Map(dto, user);

                var result = _userManager.UpdateAsync(user).Result;

                if (!result.Succeeded) throw new Exception(string.Join(" ", result.Errors.Select(x => x.Description)));

                return _autoMapper.Map<UserDto>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to update the user. Error: {ex}", ex);
                throw;
            }
        }

        public override UserDto Update(UserDto dto, Func<UserModel, bool> primaryKey)
        {
            throw new NotImplementedException("Please use method Update(UserDto dto, Guid id)");
        }

        public override void Delete(Guid userId)
        {
            try
            {
                var user = _userManager.FindByIdAsync(userId.ToString()).Result;

                var result = _userManager.DeleteAsync(user).Result;

                if (!result.Succeeded) throw new Exception(string.Join(" ", result.Errors.Select(x => x.Description)));
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to delete the user. Error: {ex}", ex);
                throw;
            }
        }

        public override void Delete(Func<UserModel, bool> primaryKey)
        {
            throw new NotImplementedException("Please use method Delete(Guid id)");
        }

        public void ChangePassword(Guid userId, string currentPassword, string newPassword)
        {
            try
            {
                var user = _userManager.FindByIdAsync(userId.ToString()).Result;

                var result = _userManager.ChangePasswordAsync(user, currentPassword, newPassword).Result;

                if (!result.Succeeded) throw new Exception(string.Join(" ", result.Errors.Select(x => x.Description)));
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to change the password of the user. Error: {ex}", ex);
                throw;
            }
        }

        public UserDto ValidateUser(Shared.Models.Login login)
        {
            var user = _userManager.FindByNameAsync(login.Username.ToUpperInvariant()).Result;

            if (user == null) throw new EntityNotFoundException($"The user with username [{login.Username}] does not exist.");

            var validationResult = _userManager.CheckPasswordAsync(user, login.Password).Result;

            if (!validationResult) throw new InvalidRequestException($"The password provided is incorrect for the user with username [{login.Username}].");

            return _autoMapper.Map<UserDto>(user);
        }
    }
}