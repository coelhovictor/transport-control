using System.Text.Json.Serialization;

namespace Core.Application.DTOs
{
    public class TransportTagDTOSet
    {
        [JsonIgnore]
        public string Id { get; set; }

        [JsonIgnore]
        public string TransportId { get; set; }

        [JsonIgnore]
        public TransportDTOGet Transport { get; set; }

        public string TagId { get; set; }

        [JsonIgnore]
        public TagDTOGet Tag { get; set; }
    }
}
