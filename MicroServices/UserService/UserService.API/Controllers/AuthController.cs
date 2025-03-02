using Microsoft.AspNetCore.Mvc;
using UserService.API.Service;

namespace UserService.API.Controllers
{
    [ApiController]
    [Route("userapi/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IdentityClientService _identityClientService;
        private readonly SessionService _sessionService;

        public AuthController(ILogger<AuthController> logger, IdentityClientService identityClientService, SessionService sessionService)
        {
            _logger = logger;
            _identityClientService = identityClientService;
            _sessionService = sessionService;
        }

        [HttpGet("session")]
        public async Task<IActionResult> CreateSession()
        {
            var response = await _identityClientService.GetSessionAsync();

            await _sessionService.SaveMemorySessionAsync(response.Data.DeviceId, response.Data.SessionId);

            return Ok(response);
        }

        [HttpGet("validate")]
        public async Task<IActionResult> ValidateSession(string deviceId, string sessionId)
        {
            bool response = await _sessionService.ValidateMemorySessionAsync(deviceId, sessionId);

            if(response)
                return Ok();

            return NotFound();
        }
        
    }
}
