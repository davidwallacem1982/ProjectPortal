using System;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class LancamentosFuturos : BaseEntity
    {
        public Int64 Cliente_ProdutosID { get; set; }
        public Cliente_Produtos Cliente_Produtos { get; set; }
        public DateTime DataVencimento { get; set; }
        public int Parcela { get; set; }
    }

}
