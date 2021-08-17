using System;
using System.Collections.Generic;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class Documentos_CTEs : BaseDocumentos
    {
        public string Origem { get; set; }
        public string Destino { get; set; }
        public string Produto { get; set; }
        public Int64 EmbarcadoresID { get; set; }
        public Embarcadores Embarcadores { get; set; }
        // Averbação
        public bool? OCD { get; set; }
        public bool? IC { get; set; }
        public bool? RI { get; set; }
        public bool? MeiosProprios { get; set; }
        public decimal? ValorContainer { get; set; }
        public decimal? ValorAcessorio { get; set; }
        public decimal? ValorAvarias { get; set; }
        public bool? RCFDC { get; set; }
        public bool? Rastreado { get; set; }
        public bool? MERCESPECIFICA { get; set; }
        public string UFFRONTEIRA { get; set; }
        public decimal? ImpostosSuspenso { get; set; }
        // File
        public bool? FileOk { get; set; }
        public string cnpjTomador { get; set; }
        public string cnpjRemetente { get; set; }
        public virtual ICollection<rel_NFs_CTEs> rel_NFs_CTEs { get; set; }
    }

}
