using System;
using System.ComponentModel.DataAnnotations;

namespace Core.API.Models
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords don`t match")]
        public string ConfirmPassword { get; set; }

        [Required]
        [MaxLength(25)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [MaxLength(25)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public DateTime? Birthdate { get; set; }

        [MaxLength(35)]
        public string Profession { get; set; }

        [MaxLength(35)]
        public string Location { get; set; }
    }
}
