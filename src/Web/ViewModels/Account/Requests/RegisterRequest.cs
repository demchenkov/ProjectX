using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Account.Requests
{
    public class RegisterRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}