using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models
{
    public class NewUserForm
    {
        [Display(Name="First Name")]
        [Required(ErrorMessage="Field is required")]
        [MinLength(3, ErrorMessage="Name fields must be at least 3 characters")]
        public string FirstName { get; set; }

        [Display(Name="Last Name")]
        [Required(ErrorMessage="Field is required")]
        [MinLength(3, ErrorMessage="Name fields must be at least 3 characters")]
        public string LastName { get; set; }

        [DisplayAttribute(Name="Email")]
        [RequiredAttribute(ErrorMessage="Field is required")]
        [EmailAddressAttribute(ErrorMessage="Invalid Email")]
        public string Email { get; set; }

        [DisplayAttribute(Name="Password")]
        [RequiredAttribute(ErrorMessage="Field is required")]
        [MinLengthAttribute(8, ErrorMessage="Password must be at least 8 characters")]
        [CompareAttribute("PasswordConfirm", ErrorMessage="Passwords do not match")]
        
        public string Password { get; set; }

        [DisplayAttribute(Name="Confirm Password")]
        [RequiredAttribute(ErrorMessage="Field is required")]
        [CompareAttribute("Password", ErrorMessage="Passwords do not match")]
        public string PasswordConfirm { get; set; }
    }

    public class LoginUserForm
    {
        [DisplayAttribute(Name="Email")]
        [RequiredAttribute(ErrorMessage="Field is required")]
        [EmailAddressAttribute(ErrorMessage="Invalid Email")]
        public string EmailLog { get; set; }

        [DisplayAttribute(Name="Password")]
        [RequiredAttribute(ErrorMessage="Field is required")]
        public string PasswordLog { get; set; }
    }
}