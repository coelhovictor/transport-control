using System;
using System.ComponentModel.DataAnnotations;

namespace Core.WebUI.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [StringLength(25, ErrorMessage = "The {0} must be at least {2} and at max " +
            "{1} characters long", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is Required")]
        [StringLength(25, ErrorMessage = "The {0} must be at least {2} and at max " +
            "{1} characters long", MinimumLength = 6)]
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
    }
}
