using System;
using System.Collections.Generic;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class Documentos_ATM_Lotes : BaseEntity
    {
        public string Lote { get; set; }
        public DateTime DataLote { get; set; }
        public int QteDocsLote { get; set; }
        public bool Redisponibilizado { get; set; }
        public int Status { get; set; } // 0: Novo Lote; 1: Confirmação Pendente; 2: Sem Documentos a Receber
        public DateTime DataInclusao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public virtual ICollection<Documentos_ATM> Documentos_ATM { get; set; }
    }
}
