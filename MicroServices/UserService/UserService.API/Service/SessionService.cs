using Microsoft.Extensions.Caching.Distributed;

namespace UserService.API.Service
{
    public class SessionService
    {
        private readonly IDistributedCache _cache;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionService(IDistributedCache cache, IHttpContextAccessor httpContextAccessor)
        {
            _cache = cache;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task SaveSessionAsync(string deviceId, string sessionId)
        {
            _httpContextAccessor.HttpContext.Session.SetString("SessionId", sessionId);
            _httpContextAccessor.HttpContext.Session.SetString("DeviceId", deviceId);
        }
        public (string sessionId, string deviceId) GetSession()
        {
            var session = _httpContextAccessor.HttpContext.Session;

            var sessionId = session.GetString("SessionId");
            var deviceId = session.GetString("DeviceId");

            return (sessionId, deviceId);
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

        public async Task SaveMemorySessionAsync(string deviceId, string sessionId)
        {
            await _cache.SetStringAsync(deviceId, sessionId, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60) // 60 dk geçerli
            });
        }

        public async Task<string?> GetMemorySessionAsync(string clientId)
        {
            return await _cache.GetStringAsync(clientId);
        }

        public async Task<bool> ValidateMemorySessionAsync(string clientId, string sessionId)
        {
            string? currentSessionId = await _cache.GetStringAsync(clientId);

            return sessionId == currentSessionId;
        }
    }

}
