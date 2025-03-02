
using MediatR;
using Newtonsoft.Json;
using ObiletService.Core.Application.Dto;
using ObiletService.Core.Application.Features.Queries.BusLocation.List;
using ObiletService.Core.Domain.Wrapper;

namespace ObiletService.Core.Application.Features.Queries.BusLocation.Search
{
    public class SearchBusLocationQueryRequest : PaginationDto,  IRequest<Response<GetBusLocationQueryResponse>>
    {
        [JsonProperty("device-session")]
        public SessionDto DeviceSesssion { get; set; }

        [JsonProperty("date")]
        public string Date {  get; set; }

        [JsonProperty("language")]
        public string Language { get; set; } = "tr-TR";

        [JsonProperty("data")]
        public string Data { get; set; }
    }
}
