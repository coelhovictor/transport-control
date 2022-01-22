using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Core.Application.DTOs
{
    public class TransportDTOSet
    {
        [JsonIgnore]
        public string Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [Range(0, 10000)]
        public decimal? Price { get; set; }

        [Required]
        public DateTime? Date { get; set; }

        [DisplayName("Arrival Date")]
        public DateTime? DateArrival { get; set; }

        [JsonIgnore]
        public ICollection<TransportTagDTOSet> TransportTags { get; set; }

        public string TransportTypeId { get; set; }

        [JsonIgnore]
        public TransportType TransportType { get; set; }

        public string ReasonId { get; set; }

        [JsonIgnore]
        public Reason Reason { get; set; }

        [JsonIgnore]
        public string UserProfileId { get; set; }

        [JsonIgnore]
        public UserProfile UserProfile { get; set; }
    }
}
