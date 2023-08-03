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
}