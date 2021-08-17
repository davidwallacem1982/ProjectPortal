using System;
using System.Collections.Generic;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class GRs : BaseCadastros
    {
        public virtual ICollection<Motoristas_Consultas> Motoristas_Consultas { get; set; }
        public virtual ICollection<Veiculos_Consultas> Veiculos_Consultas { get; set; }
        // Gerenciadora
        public virtual ICollection<Clientes_GRs> Clientes_GRs { get; set; }
    }

}
