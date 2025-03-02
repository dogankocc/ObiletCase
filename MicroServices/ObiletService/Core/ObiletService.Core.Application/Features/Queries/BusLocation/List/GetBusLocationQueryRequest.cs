
using MediatR;
using Newtonsoft.Json;
using ObiletService.Core.Application.Dto;
using ObiletService.Core.Domain.Wrapper;

namespace ObiletService.Core.Application.Features.Queries.BusLocation.List
{
    public class GetBusLocationQueryRequest : PaginationDto,  IRequest<Response<GetBusLocationQueryResponse>>
    {
        [JsonProperty("device-session")]
        public SessionDto DeviceSesssion { get; set; }

        [JsonProperty("date")]
        public string Date {  get; set; }

        [JsonProperty("language")]
        public string Language { get; set; } = "tr-TR";
    }
}
