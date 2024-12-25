using System.ComponentModel.DataAnnotations;

namespace DrugStore.DTOs.User
{
    public class RegisterViewModel
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please enter {0}")]
        [MaxLength(100, ErrorMessage = "{0} cannot be more than {1} characters.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please enter {0}")]
        [MaxLength(100, ErrorMessage = "{0} cannot be more than {1} characters.")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [MaxLength(200, ErrorMessage = "{0} cannot be more than {1} characters.")]
        [Required(ErrorMessage = "Please enter {0}")]
        [EmailAddress(ErrorMessage = "The entered email is not valid.")]
        public string Email { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Please enter {0}!")]
        [MaxLength(13, ErrorMessage = "{0} cannot be more than {1} characters.")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter {0}")]
        [MaxLength(200, ErrorMessage = "{0} cannot be more than {1} characters.")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Please enter {0}")]
        [MaxLength(200, ErrorMessage = "{0} cannot be more than {1} characters.")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string RePassword { get; set; }

        [Display(Name = "Site Rules")]
        [Required(ErrorMessage = "Please check {0}")]
        public bool RuleAccept { get; set; }
    }

    public class LoginViewModel
    {
        [Display(Name = "Email")]
        [MaxLength(200, ErrorMessage = "{0} cannot be more than {1} characters.")]
        [Required(ErrorMessage = "Please enter your {0}")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter {0}")]
        [MaxLength(200, ErrorMessage = "{0} cannot be more than {1} characters.")]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RemindMe { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter {0}")]
        [MaxLength(200, ErrorMessage = "{0} cannot be more than {1} characters.")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Please enter {0}")]
        [MaxLength(200, ErrorMessage = "{0} cannot be more than {1} characters.")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string RePassword { get; set; }
    }

}
