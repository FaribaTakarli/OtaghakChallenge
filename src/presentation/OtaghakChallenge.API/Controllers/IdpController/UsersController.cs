

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtaghakChallenge.API.Controllers.IdpController.Models;
using OtaghakChallenge.Application.Idp;
using OtaghakChallenge.Domain.Idp;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly ITokenService _tokenService;

    public UsersController(ITokenService tokenService)
    {
        this._tokenService = tokenService;
    }

    
    [AllowAnonymous]
    [HttpPost("GetToken")]
   // [Route("authenticate")]
    public IActionResult Authenticate(UserModel user)
    {
        var token = _tokenService.Authenticate(new User() {Name=user.Name ,Password=user.Password  });

        if (token == null)
        {
            return Unauthorized();
        }

        return Ok(token);
    }
}

