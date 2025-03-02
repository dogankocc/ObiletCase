using Newtonsoft.Json;
using ObiletService.Core.Application.Dto;

namespace ObiletService.Core.Application.Features.Queries.BusLocation.List
{
    public class GetBusLocationQueryResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public List<BusLocationDto> Data { get; set; }
    }
}
