using System;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class Menus_TipoAcesso : BaseEntity
    {
        public int TiposAcessosId { get; set; }
        public Int64 MenusID { get; set; }
    }

}
