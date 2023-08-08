using LucianCasino.DBObjects.DTO;
using LucianCasino.Services;
using Microsoft.AspNetCore.Mvc;

namespace LucianCasino.Controllers;

[Route("lc/users")]
public class DbUserController : Controller
{
    private readonly FirebaseAuthService _userService;

    public DbUserController(FirebaseAuthService userService)
    {
        _userService = userService;
    }

    [Route("add/single")]
    [HttpPost]
    public async Task<string?> AddUserAsync([FromBody] UserDTO userDto)
    {
        return await _userService.SignUp(userDto);
    }
}