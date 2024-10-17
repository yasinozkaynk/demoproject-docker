using System.ComponentModel.DataAnnotations;

namespace demoproject.Models.ViewModels
{
    public record class SignUpViewModel([Required] string UserName, [Required] string Email, [Required] string Password, string ConfirmPassword, [Required] string PhoneNumber);

}
