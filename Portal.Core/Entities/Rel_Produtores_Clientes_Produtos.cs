using System;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class Rel_Produtores_Clientes_Produtos : BaseEntity
    {
        public Int64 Cliente_ProdutosID { get; set; }
        public Cliente_Produtos Clientes_Produtos { get; set; }
        public Int64 ProdutoresID { get; set; }
        public Produtores Produtores { get; set; }
        public bool Principal { get; set; }
        public bool Manutecao { get; set; }
        public decimal Porcentual { get; set; }
    }

}
