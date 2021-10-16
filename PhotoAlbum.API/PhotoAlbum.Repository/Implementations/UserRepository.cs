using AutoMapper;
using Microsoft.Extensions.Logging;
using PhotoAlbum.Data;
using PhotoAlbum.Repository.Implementations.Base;
using PhotoAlbum.Repository.Interfaces;

using UserDto = PhotoAlbum.Shared.Models.User;
using UserModel = PhotoAlbum.Data.Models.ApplicationUser;

namespace PhotoAlbum.Repository.Implementations
{
    public class UserRepository : SearchableRepository<UserDto, UserModel>, IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _autoMapper;
        private readonly ILogger _logger;

        public UserRepository(ApplicationDbContext db, ILogger<UserRepository> logger, IMapper autoMapper) : base(db, logger, autoMapper)
        {
            _db = db;
            _logger = logger;
            _autoMapper = autoMapper;
        }
    }
}