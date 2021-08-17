using System.ComponentModel.DataAnnotations;

namespace Portal.Web.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Usuário")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    //   public class LoginViewModel
    //{
    //	[Required]
    //	[EmailAddress]
    //	public string Email { get; set; }

    //	[Required]
    //	[DataType(DataType.Password)]
    //	public string Password { get; set; }

    //	[Display(Name = "Remember me?")]
    //	public bool RememberMe { get; set; }
    //}
}
