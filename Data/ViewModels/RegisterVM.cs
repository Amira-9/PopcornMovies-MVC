namespace eTickets.Data.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Full Name is required")]
        [Display(Name = "Full Name")]
        public string  FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email Address")]
        public string  EmailAddress { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string  Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string  ConfirmPassword { get; set; }
    }
}
