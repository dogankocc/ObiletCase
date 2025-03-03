

namespace ObiletService.Core.Application.Dto
{
    public class PartnerDto
    {
        public long Id { get; set; }

        public string PartnerName { get; set; }

        public string OriginLocation { get; set; }

        public string DestinationLocation { get; set; }
        public long OriginLocationId { get; set; }
        public long DestinationLocationId { get; set; }
        public DateTime Departure { get; set; }

        public List<JourneyDto> Journeys { get; set; }
    }
    public class JourneyDto
    {
        public string Origin { get; set; }

        public string Destination { get; set; }

        public string Departure { get; set; }

        public string Arrival { get; set; }

        public string Price { get; set; }
    }
}
