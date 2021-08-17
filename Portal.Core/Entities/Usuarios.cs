using System;
using System.Collections.Generic;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class Usuarios : BaseEntity
    {
        public string Nome { get; set; }
        public string userName { get; set; }
        public bool bloqueioIP { get; set; }
        public bool? Interno { get; set; }
        public Int64 TipoAcessosID { get; set; }
        public bool Ativo { get; set; }
        // Usuários
        public virtual ICollection<rel_Usuarios_Clientes> rel_Usuarios_Clientes { get; set; }
    }
}
