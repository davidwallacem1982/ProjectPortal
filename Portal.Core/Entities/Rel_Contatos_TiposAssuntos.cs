using System;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class Rel_Contatos_TiposAssuntos : BaseEntity
    {
        public Int64 TiposAssuntosID { get; set; }
        public Int64 ContatosID { get; set; }
    }

}
