using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels.Accounts
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O campo Email é obrigatório")]
        [EmailAddress(ErrorMessage = "O email informado não é valido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        public string Password { get; set; }
    }
}
