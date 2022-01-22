using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Core.Application.DTOs
{
    public class TransportTypeDTOGet
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal? Price { get; set; }

        public string Color { get; set; }

        [JsonIgnore]
        public ICollection<Transport> Transports { get; set; }

        [JsonIgnore]
        public string UserProfileId { get; set; }

        [JsonIgnore]
        public UserProfile UserProfile { get; set; }

        [DisplayName("Created At")]
        public DateTime? CreatedAt { get; set; }
    }
}
