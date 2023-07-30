using LucianCasino.DBObjects;
using LucianCasino.Services;
using Microsoft.AspNetCore.Mvc;

namespace LucianCasino.Controllers;

[Route("lc/users/")]
[ApiController]
public class UserController : Controller
{
    private readonly UserService _userService = new UserService();

    [Route("all")]
    [HttpGet]
    public async Task<ActionResult<ICollection<User>>> GetUsers()
    {
        IDictionary<string, User> users = await _userService.GetUsersAsync();
        return Ok(users.Values);
    }

    [Route("add/single")]
    [HttpPost]
    public async Task<ActionResult<string>> AddUser(User user)
    {
        string id = await _userService.AddUserAsync(user);
        return id;
    }
}