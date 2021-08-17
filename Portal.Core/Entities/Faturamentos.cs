using System;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class Faturamentos : BaseEntity
    {
        public Int64 Cliente_ProdutosID { get; set; }
        public Cliente_Produtos Cliente_Produtos { get; set; }
        public decimal? Valor { get; set; }
        public decimal? ImportanciaSegurada { get; set; }
        public bool Estimativa { get; set; }
        public DateTime DataMovimento { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime Datainclusao { get; set; }
        // 10/05/2016
        public decimal? ValorBruto { get; set; }
        public decimal? ValorAcrescimo { get; set; }
        public decimal? ValorDesconto { get; set; }
        public decimal? ValorImposto { get; set; }
        public DateTime? DiaAlteracao { get; set; }
        public int? QuantParcelas { get; set; }
        public int? NumeroParcelas { get; set; }
        public bool? Pago { get; set; }
        public bool? Habilitada { get; set; }
    }
}
