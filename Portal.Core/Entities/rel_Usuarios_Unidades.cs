using System;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class rel_Usuarios_Unidades : BaseEntity
    {
        public string UserID { get; set; }

        public Int16 TipoUnidade { get; set; } // 0: Embarcador; 1: Transportador; 2: Destinatário; 3: Gerenciador de Risco;

        public Int64 Identificador { get; set; }
    }

}
