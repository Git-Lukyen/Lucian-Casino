using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LucianCasino.Controllers;

[Route("/lc")]
[ApiController]
public class TestController : Controller
{

    [Authorize]
    [Route("")]
    [HttpGet]
    public string TestRequest()
    {
        return "works";
    }
    
}