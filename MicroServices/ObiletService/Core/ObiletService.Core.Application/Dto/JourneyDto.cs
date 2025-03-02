
using Newtonsoft.Json;

namespace ObiletService.Core.Application.Dto
{
    public class JourneyDto
    {
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }


        public decimal TicketPrice { get; set; }
    }

}
