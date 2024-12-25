using System.ComponentModel.DataAnnotations;

namespace DrugStore.DTOs.User
{
    public class CreateUserViewModel
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Please enter {0}")]
        [MaxLength(200, ErrorMessage = "{0} cannot be more than {1} characters.")]
        public string UserName { get; set; }

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

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter {0}")]
        [MaxLength(200, ErrorMessage = "{0} cannot be more than {1} characters.")]
        public string Password { get; set; }

        public string? Address { get; set; }
        public string? PostalCode { get; set; }
        public bool IsEmailActive { get; set; }

        public List<int> SelectedRoles { get; set; }
    }
    public class EditUserViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }

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

        [Display(Name = "Password")]
        [MaxLength(200, ErrorMessage = "{0} cannot be more than {1} characters.")]
        public string? Password { get; set; }

        public string? Address { get; set; }
        public string? PostalCode { get; set; }
        public bool IsEmailActive { get; set; }

        public List<int>? SelectedRoles { get; set; }
    }
}
