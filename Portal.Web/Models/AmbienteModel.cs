using System;

namespace Portal.Web.Models
{
    public class AmbienteModel
    {
        //Id do Usuário
        public string Id { get; set; }
        public string NomeUsuario { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public long ClientesID { get; set; }
    }
    public class UserToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
