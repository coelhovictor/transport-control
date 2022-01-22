﻿using System.ComponentModel.DataAnnotations;

namespace Core.WebUI.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [StringLength(25, ErrorMessage = "The {0} must be at least {2} and at max " +
            "{1} characters long", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
