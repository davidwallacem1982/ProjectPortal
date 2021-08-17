using System;

namespace Portal.Core.Entities
{
    public class Documentos_Sinistros : BaseEntity
    {
        public Int64 SinistrosID { get; set; }
        public Sinistros Sinistros { get; set; }
        // Documentos no MaisProteção
        public Int64 DocumetosID { get; set; }
        public int TiposDocumentos { get; set; } // 0: NFe; 1: CTe; 2: MDFe;3: Tnix20; 4: OB
        public string numeroDocumentos { get; set; }
        public string NomeEmitente { get; set; }
        public DateTime? DataEmissao { get; set; }
        public decimal? Valor { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }
        public string Observacao { get; set; }
        public string pathFile { get; set; }
        public string EmailSolicitacao { get; set; }
        public DateTime Datainclusao { get; set; }
        public DateTime? DataSolicitacao { get; set; }
        public DateTime? DataAteracao { get; set; }
        public DateTime? DataAceite { get; set; }
        public Int16 Situacao { get; set; } // 0: Solicitado;1: Enviado arquivo;2: Arquivo OK;3: Arquivo Não valido;
    }
}
