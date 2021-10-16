using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoAlbum.API.Controllers.Base;
using PhotoAlbum.Logic.Interfaces;

namespace PhotoAlbum.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController<IUserService>
    {
        private readonly ILogger _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService) : base(logger, userService)
        {
            _logger = logger;
            _userService = userService;
        }
    }
}
