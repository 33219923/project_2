using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoAlbum.API.Controllers.Base;
using PhotoAlbum.Logic.Interfaces;
using PhotoAlbum.Shared.Models;

namespace PhotoAlbum.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController<IUserService, User>
    {
        private readonly ILogger _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService) : base(logger, userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost]
        [Route("token")]
        public ActionResult<string> Token([FromBody] Login login)
        {
            _logger.LogDebug("User controller token called. Login: {@login}", login);
            var result = _userService.GetToken(login);
            return Ok(result);
        }
    }
}
