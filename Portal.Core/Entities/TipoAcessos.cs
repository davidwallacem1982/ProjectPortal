using System;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class TipoAcessos : BaseEntity
    {
        public string TipoAcesso { get; set; }
        public Int64 ClientesID { get; set; }
    }

}
