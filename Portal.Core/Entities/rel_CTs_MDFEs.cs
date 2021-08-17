using System;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class rel_CTs_MDFEs : BaseEntity
    {
        public Int64 Documentos_MDFEsID { get; set; }
        public Documentos_MDFEs Documentos_MDFEs { get; set; }
        // CTe
        public string Chave_CTe { get; set; }
        // CT (Papel)
        public string numeroCT { get; set; }
        public DateTime? DataEmissao { get; set; }
        public decimal? ValorCT { get; set; }
    }

}
