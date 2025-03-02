using ObiletService.Core.Domain.Seedwork;
using ObiletService.Core.Domain.ValueObjects;

namespace ObiletService.Core.Domain.Entities
{
    public class Journey : BaseEntity, IAggregateRoot
    {
        public Vehicle Vehicle { get; private set; }
        public Location DepartureLocation { get; private set; }
        public Location ArrivalLocation { get; private set; }
        public DateTime DepartureTime { get; private set; }
        public DateTime ArrivalTime { get; private set; }
        public Price TicketPrice { get; private set; }

        public Journey(Vehicle vehicle, Location departureLocation, Location arrivalLocation, DateTime departureTime, DateTime arrivalTime, Price ticketPrice)
        {
            Vehicle = vehicle;
            DepartureLocation = departureLocation;
            ArrivalLocation = arrivalLocation;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
            TicketPrice = ticketPrice;
        }
    }
}
