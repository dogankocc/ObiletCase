using Grpc.Core;
using IdentityService.API.Helper;
using Newtonsoft.Json;
using System.Text;

namespace IdentityService.API.Service
{
    public class IdentityServiceImpl : Identity.IdentityBase
    {
        public override async Task<SessionResponse> GetSession(SessionRequest request, ServerCallContext context)
        {
            Console.WriteLine($"Bağlanan IP: {request.Connection.IpAddress}");
            Console.WriteLine($"Cihaz ID: {request.Application.EquipmentId}");

            // HttpClient nesnesi oluşturuluyor
            using (var client = new HttpClient())
            {
                // API URL
                var url = "https://v2-api.obilet.com/api/client/getsession";

                // Başlıkları ekliyoruz
                client.DefaultRequestHeaders.Add("Authorization", "Basic JEcYcEMyantZV095WVc3G2JtVjNZbWx1");

                var jsonString = JsonConvert.SerializeObject(request, new ProtoRequestJsonConverter());

                // İçeriği HttpContent türüne dönüştürüp, gönderiyoruz
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                try
                {
                    // POST isteğini gönderiyoruz
                    var response = await client.PostAsync(url, content);

                    // Yanıtı alıyoruz
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();

                    var responseModel = JsonConvert.DeserializeObject<SessionResponse>(responseBody, new ProtoResponseJsonConverter());

                    return responseModel;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Hata: " + ex.Message);

                    return new SessionResponse
                    {
                        Status = "Failed",
                        Message = ex.Message
                    };
                }
            }
       
        }
    }
}
