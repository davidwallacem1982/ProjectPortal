using System;
using System.Collections.Generic;

namespace Portal.Core.Entities
{

    [Serializable()]
    public class Embarcadores : BaseCadastros
    {
        public Int64 ClientesID { get; set; }
        public string IE { get; set; }
        public string NomeFantasia { get; set; }
        public virtual ICollection<Documentos_NFEs> Documentos_NFEs { get; set; }
        public virtual ICollection<Documentos_CTEs> Documentos_CTEs { get; set; }
    }

}
