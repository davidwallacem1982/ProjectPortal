using System;
using System.Collections.Generic;

namespace Portal.Core.Entities
{
    public class Unidades : BaseEntity
    {
        public Int64 ClientesID { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public Int16 Tipo { get; set; }
        public Clientes Clientes { get; set; }
        public virtual ICollection<Sinistros> Sinistros { get; set; }

    }
}
