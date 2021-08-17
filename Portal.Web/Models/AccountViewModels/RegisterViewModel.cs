using System.ComponentModel.DataAnnotations;

namespace Portal.Web.Models.AccountViewModels
{

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Usuário")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Regra")]
        public string Role { get; set; }

        [Required]
        [Display(Name = "Tipo Acessos")]
        public long TipoAcessosID { get; set; }

        [Required]
        [Display(Name = "Segurado")]
        public long ClientesID { get; set; }


        [Required]
        [Display(Name = "Bloqueio Por IP")]
        public bool bloqueioIP { get; set; }

        [Required]
        [Display(Name = "Interno")]
        public bool Interno { get; set; }

        [Required]
        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }

        [Required]
        [Display(Name = "Tipo")]
        public bool Tipo { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A {0} deve ter no mínimo de {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmação da Senha")]
        [Compare("Password", ErrorMessage = "A sua senha e a confirmação da senha estão diferentes.")]
        public string ConfirmPassword { get; set; }
    }

    //   public class RegisterViewModel
    //{
    //	[Required]
    //	[EmailAddress]
    //	[Display(Name = "Email")]
    //	public string Email { get; set; }

    //	[Required]
    //	[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    //	[DataType(DataType.Password)]
    //	[Display(Name = "Password")]
    //	public string Password { get; set; }

    //	[DataType(DataType.Password)]
    //	[Display(Name = "Confirm password")]
    //	[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    //	public string ConfirmPassword { get; set; }
    //}
}
