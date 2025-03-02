using ObiletService.Core.Domain.Seedwork;

namespace ObiletService.Core.Domain.ValueObjects
{
    public class Location : ValueObject
    {
        public string Name { get; set; }
        public double Latitude { get; }
        public double Longitude { get; }
        public Location(double latitude, double longitude, string name)
        {
            Latitude = latitude;
            Longitude = longitude;
            Name = name;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}
