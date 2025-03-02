using Newtonsoft.Json;
using ObiletService.Core.Application.Dto;

namespace ObiletService.Core.Application.Features.Queries.Journeys.List
{
    public class GetJourneysQueryResponse : PartnerDto
    {


    }

    public class JourneyResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public List<GetJourneysData> Data { get; set; }
    }

    public class GetJourneysData
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("partner-name")]
        public string PartnerName { get; set; }

        [JsonProperty("origin-location")]
        public string OriginLocation { get; set; }

        [JsonProperty("destination-location")]
        public string DestinationLocation { get; set; }

        [JsonProperty("journey")]
        public Journey Journey { get; set; }
    }

    public class Journey
    {

        [JsonProperty("origin")]
        public string Origin { get; set; }

        [JsonProperty("destination")]
        public string Destination { get; set; }

        [JsonProperty("departure")]
        public DateTime Departure { get; set; }

        [JsonProperty("arrival")]
        public DateTime Arrival { get; set; }

        [JsonProperty("internet-price")]
        public decimal Price { get; set; }
    }
}
