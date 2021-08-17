using System.ComponentModel.DataAnnotations;

namespace Portal.Web.Models.AccountViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required(ErrorMessage = "Preencha o campo da senha.")]
        [StringLength(15, ErrorMessage = "O {0} deve ter pelo menos {2} e no máximo {1} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "A senha e a senha de confirmação não coincidem.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
