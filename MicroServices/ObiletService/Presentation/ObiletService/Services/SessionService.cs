using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;

namespace ObiletService.Services
{
    public class SessionService
    {
        private readonly IdentityClientService _identityClientService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionService(IdentityClientService identityClientService, IHttpContextAccessor httpContextAccessor) 
        {
            _identityClientService = identityClientService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> CreateSessionAsync()
        {
            var response = await _identityClientService.GetSessionAsync();

            if (response != null && response.Status == "Success")
            {
                _httpContextAccessor.HttpContext.Session.SetString("SessionId", response.Data.SessionId);
                _httpContextAccessor.HttpContext.Session.SetString("DeviceId", response.Data.DeviceId);

                return true;
            }
            return false;
        }

        public async Task<bool> ValidateSessionAsync(string deviceId, string sessionId)
        {
            var session = _httpContextAccessor.HttpContext.Session;

            if (session.GetString("SessionId") == sessionId && session.GetString("DeviceId") == deviceId)
            {
                return true;
            }
            return false;
        }
    }
}
