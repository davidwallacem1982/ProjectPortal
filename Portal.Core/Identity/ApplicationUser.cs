using Microsoft.AspNetCore.Identity;

namespace Portal.Core.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public bool bloqueioIP { get; set; }
        public string Nome { get; set; }
        public bool Interno { get; set; }
        public long TipoAcessoID { get; set; }
        public long ClientesID { get; set; }
        public bool Acesso { get; set; }
    }
}
