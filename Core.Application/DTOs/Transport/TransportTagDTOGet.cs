using System.Text.Json.Serialization;

namespace Core.Application.DTOs
{
    public class TransportTagDTOGet
    {
        [JsonIgnore]
        public string Id { get; set; }

        [JsonIgnore]
        public string TransportId { get; set; }

        [JsonIgnore]
        public TransportDTOGet Transport { get; set; }

        [JsonIgnore]
        public string TagId { get; set; }

        public TagDTOGet Tag { get; set; }
    }
}
