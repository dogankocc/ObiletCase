using Microsoft.AspNetCore.Mvc;
using ObiletService.Services;

namespace ObiletService.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly SessionService _sessionService;

        public UserController(ILogger<UserController> logger, SessionService sessionService)
        {
            _logger = logger;
            _sessionService = sessionService;
        }

        [HttpGet("session")]
        public async Task<IActionResult> CreateSession()
        {     
            
            if (await _sessionService.CreateSessionAsync())
            {            
                return RedirectToAction("Index", "Home");
            }
            return View("~/Views/Error.cshtml");
        }
    }
}
