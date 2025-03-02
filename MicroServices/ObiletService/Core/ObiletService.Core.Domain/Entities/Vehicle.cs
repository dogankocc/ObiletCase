using ObiletService.Core.Domain.Seedwork;

namespace ObiletService.Core.Domain.Entities
{
    public class Vehicle : BaseEntity, IAggregateRoot
    {
        public string Model { get; private set; }
        public int Capacity { get; private set; }

        protected Vehicle(string model, int capacity)
        {
            Model = model;
            Capacity = capacity;
        }
    }
}
