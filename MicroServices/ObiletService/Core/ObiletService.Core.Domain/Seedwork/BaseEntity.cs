
namespace ObiletService.Core.Domain.Seedwork
{
    public class BaseEntity
    {
        private Guid _id;
        public Guid Id { get { return _id;  } private set { _id = Guid.NewGuid(); } }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }
    }
}
