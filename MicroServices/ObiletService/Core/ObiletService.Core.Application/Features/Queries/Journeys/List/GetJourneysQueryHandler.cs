
using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using ObiletService.Core.Application.Dto;
using ObiletService.Core.Domain.Wrapper;
using System.Text;

namespace ObiletService.Core.Application.Features.Queries.Journeys.List
{
    public class GetJourneysQueryHandler : IRequestHandler<GetJourneysQueryRequest, Response<GetJourneysQueryResponse>>
    {
        private readonly IMapper _mapper;
        public GetJourneysQueryHandler(IMapper mapper) { _mapper = mapper; }

        public async Task<Response<GetJourneysQueryResponse>> Handle(GetJourneysQueryRequest request, CancellationToken cancellationToken)
        {
            using (HttpClient client = new HttpClient())
            {

                string jsonContent = JsonConvert.SerializeObject(request, new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver() // Özel camelCase vs. olmasın
                });

                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                client.DefaultRequestHeaders.Add("Authorization", "Basic JEcYcEMyantZV095WVc3G2JtVjNZbWx1");

                HttpResponseMessage httpResponse = await client.PostAsync("https://v2-api.obilet.com/api/journey/getbusjourneys", content);

                string responseBody = await httpResponse.Content.ReadAsStringAsync();

                var response = JsonConvert.DeserializeObject<JourneyResponse>(responseBody);

                var result = response.Data.GroupBy(e => new
                {
                    e.OriginLocation,
                    e.DestinationLocation,
                    e.OriginLocationId,
                    e.DestinationLocationId,
                    Departure = e.Journey.Departure.Date
                }).Select(e => new GetJourneysQueryResponse
                {
                    OriginLocationId = e.Key.OriginLocationId,
                    DestinationLocation = e.Key.DestinationLocation,
                    OriginLocation = e.Key.OriginLocation,
                    DestinationLocationId = e.Key.DestinationLocationId,
                    Departure = e.Key.Departure,
                    Journeys = _mapper.Map<List<JourneyDto>>(e.Select(s => s.Journey).ToList()).OrderBy(e=>e.Departure).ToList()
                }).FirstOrDefault();

                return new Response<GetJourneysQueryResponse>
                {
                    IsSuccessful = true,
                    Result = result
                };
            }
        }
    }
}
