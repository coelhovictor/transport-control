using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Core.Application.DTOs
{
    public class UserProfileDTOSet
    {
        [JsonIgnore]
        public string Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [MaxLength(25)]
        public string FirstName { get; set; }

        [MaxLength(25)]
        public string LastName { get; set; }

        public DateTime? Birthdate { get; set; }

        [MaxLength(35)]
        public string Profession { get; set; }

        [MaxLength(35)]
        public string Location { get; set; }

    }
}
