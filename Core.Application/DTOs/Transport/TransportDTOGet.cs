using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Core.Application.DTOs
{
    public class TransportDTOGet
    {
        public string Id { get; set; }
        public string Name { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DataType(DataType.Currency)]
        public decimal? Price { get; set; }

        public DateTime Date { get; set; }

        [DisplayName("Arrival Date")]
        public DateTime? DateArrival { get; set; }

        public ICollection<TransportTagDTOGet> TransportTags { get; set; }

        [JsonIgnore]
        public string TransportTypeId { get; set; }

        public TransportTypeDTOGet TransportType { get; set; }

        [JsonIgnore]
        public string ReasonId { get; set; }

        public ReasonDTOGet Reason { get; set; }

        [JsonIgnore]
        public string UserProfileId { get; set; }

        [JsonIgnore]
        public UserProfile UserProfile { get; set; }
    }
}
