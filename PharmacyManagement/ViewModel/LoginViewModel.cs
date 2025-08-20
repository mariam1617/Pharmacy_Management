using System.ComponentModel.DataAnnotations;

namespace PharmacyManagement.ViewModel
{
    public class LoginViewModel

    {
        [Required(ErrorMessage = "Username is required.")]

        public string Username { get; set; }


        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; } = false;
        public LoginViewModel()
        {
            Username = string.Empty;
            Password = string.Empty;
        }
        
    }
}
