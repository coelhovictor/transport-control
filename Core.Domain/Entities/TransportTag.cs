using Core.Domain.Validators;

namespace Core.Domain.Entities
{
    public sealed class TransportTag : Entity
    {
        public string TransportId { get; set; }

        public Transport Transport { get; set; }

        public string TagId { get; set; }

        public Tag Tag { get; set; }

        public TransportTag()
        {
        }
    }
}
