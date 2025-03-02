
using Newtonsoft.Json;

namespace ObiletService.Core.Application.Dto
{
    public class SessionDto
    {
        [JsonProperty("device-id")]
        public string DeviceId { get; set; }
        [JsonProperty("session-id")]
        public string SessionId { get; set; }
    }
}
