using Grpc.Net.Client;
using System.Net;

namespace UserService.API.Service
{
    public class IdentityClientService
    {
        private readonly Identity.IdentityClient _client;

        public IdentityClientService()
        {
            // IdentityService gRPC servisine bağlan
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            _client = new Identity.IdentityClient(channel);
        }

        public async Task<SessionResponse> GetSessionAsync()
        {
            var request = new SessionRequest
            {
                Type = 7,
                Connection = new Connection { IpAddress = GetLocalIPAddress(), Port = "5002" },
                Application = new Application { Version = "1.0.0.0", EquipmentId = "distribusion" },
                Browser = new Browser { Name = "Chrome", Version = "47.0.0.12" }
            };

            return await _client.GetSessionAsync(request);
        }

        static string GetLocalIPAddress()
        {
            string localIP = string.Empty;

            foreach (var ip in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }

            return localIP;
        }
    }
}
