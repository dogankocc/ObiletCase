using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace IdentityService.API.Helper
{
    public class ProtoResponseJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(SessionResponse); // İlgili türü belirtiyoruz
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);

            return new SessionResponse
            {
                Status = (string)obj["status"],
                Message = obj["message"]?.ToString(),
                Data = new SessionData
                {
                    SessionId = (string)obj["data"]["session-id"],
                    DeviceId = (string)obj["data"]["device-id"]
                }
            };
        }
    }
}
