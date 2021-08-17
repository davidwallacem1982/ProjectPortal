using System;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class rel_Usuarios_Clientes : BaseEntity
    {
        public string UserID { get; set; }
        public Int64 ClientesID { get; set; }
    }

}
