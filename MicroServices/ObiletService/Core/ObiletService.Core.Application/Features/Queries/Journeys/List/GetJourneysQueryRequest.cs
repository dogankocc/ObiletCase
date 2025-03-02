
using MediatR;
using Newtonsoft.Json;
using ObiletService.Core.Application.Dto;
using ObiletService.Core.Domain.Wrapper;

namespace ObiletService.Core.Application.Features.Queries.Journeys.List
{
    public class GetJourneysQueryRequest : PaginationDto,  IRequest<Response<GetJourneysQueryResponse>>
    {
        [JsonProperty("device-session")]
        public SessionDto DeviceSesssion { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; } = "tr-TR";

        [JsonProperty("data")]
        public GetJourneysRequestData Data { get; set; }
    }

    public class GetJourneysRequestData
    {
        [JsonProperty("origin-id")]
        public long OriginId { get; set; }

        [JsonProperty("destination-id")]
        public long DestinationId { get; set; }

        [JsonProperty("departure-date")]
        public string DepartureDate { get; set; }
    }
}
