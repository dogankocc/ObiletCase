
using MediatR;
using ObiletService.Core.Domain.Wrapper;
using System.Text;

namespace ObiletService.Core.Application.Features.Queries.Journeys.List
{
    public class GetJourneysQueryHandler : IRequestHandler<GetJourneysQueryRequest, Response<GetJourneysQueryResponse>>
    {
        public async Task<Response<GetJourneysQueryResponse>> Handle(GetJourneysQueryRequest request, CancellationToken cancellationToken)
        {
            using (HttpClient client = new HttpClient())
            {

                var requestData = new
                {
                    data = (object)null,
                    device_session = new
                    {
                        session_id = request.Session.SessionId,
                        device_id = request.Session.DeviceId
                    },
                    date = DateTime.Now,
                    language = "tr-TR"
                };

                string jsonContent = System.Text.Json.JsonSerializer.Serialize(requestData);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                client.DefaultRequestHeaders.Add("Authorization", "Basic JEcYcEMyantZV095WVc3G2JtVjNZbWx1");

                HttpResponseMessage response = await client.PostAsync("https://v2-api.obilet.com/api/location/getbuslocations", content);

                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);

                return null;
            }
        }
    }
}
