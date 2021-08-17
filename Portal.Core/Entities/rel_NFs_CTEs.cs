using System;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class rel_NFs_CTEs : BaseEntity
    {
        public Int64 Documentos_CTEsID { get; set; }
        public Documentos_CTEs Documentos_CTEs { get; set; }

        // NFe
        public string Chave_NFe { get; set; }

        // NF (Papel)
        public string cnpjEmitente { get; set; }
        public string cnpjDestinatario { get; set; }
        public string numeroNF { get; set; }
        public DateTime? DataEmissao { get; set; }
        public decimal? ValorNF { get; set; }
        public string Giro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
    }

}
