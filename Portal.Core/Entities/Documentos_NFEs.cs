using System;

namespace Portal.Core.Entities
{
    [Serializable]
    public class Documentos_NFEs : BaseDocumentos
    {
        public string Origem { get; set; }
        public string Destino { get; set; }
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
    }

}
