using System;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class Rel_Seguradoras_Clientes_Produtos : BaseEntity
    {
        public Int64 Cliente_ProdutosID { get; set; }
        public Cliente_Produtos Clientes_Produtos { get; set; }
        public Int64 SeguradorasID { get; set; }
        public Seguradoras Seguradoras { get; set; }
    }

}
