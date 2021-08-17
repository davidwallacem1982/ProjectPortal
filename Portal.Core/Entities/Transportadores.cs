using System;
using System.Collections.Generic;

namespace Portal.Core.Entities
{

    [Serializable()]
    public class Transportadores : BaseCadastros
    {
        public Int64 ClientesID { get; set; }
        public string IE { get; set; }
        public string NomeFantasia { get; set; }

        public virtual ICollection<Documentos_NFEs> Documentos_NFEs { get; set; }
        public virtual ICollection<Documentos_CTEs> Documentos_CTEs { get; set; }
        public virtual ICollection<Documentos_MDFEs> Documentos_MDFEs { get; set; }

        public virtual ICollection<Sinistros> Sinistros { get; set; }

        public virtual ICollection<Motoristas_Consultas> Motoristas_Consultas { get; set; }
        public virtual ICollection<Veiculos_Consultas> Veiculos_Consultas { get; set; }

        public virtual ICollection<Transportadores_Veiculos> Transportadores_Veiculos { get; set; }
        public virtual ICollection<Transportadores_Motoristas> Transportadores_Motoristas { get; set; }
    }
}
