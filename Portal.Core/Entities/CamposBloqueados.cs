using System;

namespace Portal.Core.Entities
{
    public class CamposBloqueados
    {
        public Int64 CamposBloqueadosID { get; set; }
        public string Prefixo { get; set; }
        public string Campo { get; set; }
        public Int64? TipoAcessosID { get; set; }
        public string UsuarioID { get; set; }
        public bool Bloqueia { get; set; }
    }
}
