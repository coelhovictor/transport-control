using AutoMapper;
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

        [IgnoreMap]
        [StringLength(25, ErrorMessage = "The {0} must be at least {2} and at max " +
            "{1} characters long", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [IgnoreMap]
        [StringLength(25, ErrorMessage = "The {0} must be at least {2} and at max " +
            "{1} characters long", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [IgnoreMap]
        [StringLength(25, ErrorMessage = "The {0} must be at least {2} and at max " +
            "{1} characters long", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords don`t match")]
        public string ConfirmPassword { get; set; }

    }
}
