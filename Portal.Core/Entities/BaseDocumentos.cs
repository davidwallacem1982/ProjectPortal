using System;

namespace Portal.Core.Entities
{
    public class BaseDocumentos : BaseEntity
    {
        public Int64 ClientesID { get; set; }
        public Clientes Clientes { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime? DataPrevisao { get; set; }
        public DateTime? DataEmbarque { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public Int64 Numero { get; set; }
        public string Serie { get; set; }
        public string OrigemUF { get; set; }
        public string DestinoUF { get; set; }
        public decimal ValorTotal { get; set; }
        public Int16 mesFiscal { get; set; }
        public Int32 anoFiscal { get; set; }
        public string Chave { get; set; }
        // Autorizada ou Cancelada
        public Int64 Situacao { get; set; }
        // Transportador
        public Int64 TransportadoresID { get; set; }
        public string TransportadorCNPJ { get; set; }
        public Transportadores Transportadores { get; set; }
        // Motoritas
        public string MotoristasCPF { get; set; }
        // Veiculos
        public string VeiculosPlaca { get; set; }
        // Averbação
        public DateTime DataInclusao { get; set; }
        // File
        public string pathFile { get; set; }
    }

}
