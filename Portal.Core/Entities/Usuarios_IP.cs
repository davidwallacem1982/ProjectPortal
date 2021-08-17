using System;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class Usuarios_IP : BaseEntity
    {
        public string UsuariosID { get; set; }
        public string userName { get; set; }
        public string IP { get; set; }
    }

}
