namespace Portal.Core.Models
{
    public class LogLoginViewModel
    {
        public string ID { get; set; }
        public string usuarioID { get; set; }
        public string UserName { get; set; }
        public string Nome { get; set; }
        public string ClientesID { get; set; }
        public string IP { get; set; }
        public string DataLogin { get; set; }
        public string DataLogout { get; set; }
        public string Logado { get; set; }
        public string LoginExpirado { get; set; }
    }
}
