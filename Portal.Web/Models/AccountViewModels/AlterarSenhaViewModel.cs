using System.ComponentModel.DataAnnotations;

namespace Portal.Web.Models.AccountViewModels
{
    public class AlterarSenhaViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string PasswordAtual { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O {0} deve ter pelo menos {2} e no máximo {1} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar de Senha")]
        [Compare("Password", ErrorMessage = "A Senha e a Confirmar de Senha não correspondem.")]
        public string ConfirmPassword { get; set; }
    }
}
