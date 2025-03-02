using Newtonsoft.Json;

namespace IdentityService.API.Helper
{
    public class ProtoRequestJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(SessionRequest); // Burada hedef sınıfı belirtirsiniz
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var sessionRequest = (SessionRequest)value;

            writer.WriteStartObject();

            // 'type' alanını yazıyoruz
            writer.WritePropertyName("type");
            writer.WriteValue(sessionRequest.Type);

            // 'connection' alanını yazıyoruz
            writer.WritePropertyName("connection");
            writer.WriteStartObject();
            writer.WritePropertyName("ip-address");  // proto'daki ad
            writer.WriteValue(sessionRequest.Connection.IpAddress);
            writer.WritePropertyName("port");
            writer.WriteValue(sessionRequest.Connection.Port);
            writer.WriteEndObject();

            // 'application' alanını yazıyoruz
            writer.WritePropertyName("application");
            writer.WriteStartObject();
            writer.WritePropertyName("version");
            writer.WriteValue(sessionRequest.Application.Version);
            writer.WritePropertyName("equipment-id"); // proto'daki ad
            writer.WriteValue(sessionRequest.Application.EquipmentId);
            writer.WriteEndObject();

            // 'browser' alanını yazıyoruz
            writer.WritePropertyName("browser");
            writer.WriteStartObject();
            writer.WritePropertyName("name");
            writer.WriteValue(sessionRequest.Browser.Name);
            writer.WritePropertyName("version");
            writer.WriteValue(sessionRequest.Browser.Version);
            writer.WriteEndObject();
            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
