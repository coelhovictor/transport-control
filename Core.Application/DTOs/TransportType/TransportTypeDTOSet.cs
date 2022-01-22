using Core.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Core.Application.DTOs
{
    public class TransportTypeDTOSet
    {
        [JsonIgnore]
        public string Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DataType(DataType.Currency)]
        [Range(0.01, 10000)]
        public decimal? Price { get; set; }

        [Required]
        [MinLength(7)]
        [MaxLength(7)]
        public string Color { get; set; }

        [JsonIgnore]
        public ICollection<Transport> Transports { get; set; }

        [JsonIgnore]
        public string UserProfileId { get; set; }

        [JsonIgnore]
        public UserProfile UserProfile { get; set; }
    }
}
