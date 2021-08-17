using System;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class rel_NFs_MDFEs : BaseEntity
    {
        public Int64 Documentos_MDFEsID { get; set; }
        public Documentos_MDFEs Documentos_MDFEs { get; set; }
        // NFe
        public string Chave_NFe { get; set; }
        // NF (Papel)
        public string cnpjEmitente { get; set; }
        public string numeroNF { get; set; }
        public DateTime? DataEmissao { get; set; }
        public decimal? ValorNF { get; set; }
    }

}
