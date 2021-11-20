using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoAlbum.API.Controllers.Base;
using PhotoAlbum.Logic.Interfaces;
using PhotoAlbum.Shared.Models;
using PhotoAlbum.Shared.Services;
using System.ComponentModel.DataAnnotations;

namespace PhotoAlbum.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController<IUserService, User>
    {
        private readonly ILogger _logger;
        private readonly IUserService _userService;
        private readonly IRequestState _requestState;

        public UserController(ILogger<UserController> logger, IUserService userService, IRequestState requestState) : base(logger, userService)
        {
            _logger = logger;
            _userService = userService;
            _requestState = requestState;
        }

        [HttpPost]
        [Route("token")]
        [AllowAnonymous]
        public ActionResult<string> Token([FromBody] Login login)
        {
            _logger.LogDebug("User controller token called. Login: {@login}", login);
            var result = _userService.GetToken(login);
            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        public override ActionResult<User> Add([FromBody] User newUser)
        {
            return base.Add(newUser);
        }

        [HttpGet]
        [Route("currentUser")]
        [Authorize]
        public ActionResult<User> CurrentUser()
        {
            var user = _userService.Get(_requestState.UserId.Value);

            return Ok(user);
        }
    }
}
