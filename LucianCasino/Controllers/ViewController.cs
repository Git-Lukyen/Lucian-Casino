using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LucianCasino.Controllers;

[Route("lc")]
public class ViewController : Controller
{
    [Route("login")]
    [HttpGet]
    public IActionResult ShowLoginPage()
    {
        return View("~/Pages/LoginPage.cshtml");
    }

    [Route("home")]
    [Authorize]
    [HttpGet]
    public IActionResult ShowHomePage()
    {
        return View("~/Pages/HomePage.cshtml");
    }
}