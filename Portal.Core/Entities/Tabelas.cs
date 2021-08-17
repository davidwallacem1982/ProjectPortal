using System;
using System.Collections.Generic;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class Tabelas : BaseEntity
    {
        public string NomeTabela { get; set; }
        public bool RegistrarLog { get; set; }
        public bool Detalhado { get; set; }
        public bool Insert { get; set; }
        public bool Update { get; set; }
        public bool Consulta { get; set; }
        public virtual ICollection<Campos> Campos { get; set; }
    }

}
