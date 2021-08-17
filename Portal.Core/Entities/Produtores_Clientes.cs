using System;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class Produtores_Clientes : BaseEntity
    {
        public Int64 ProdutoresID { get; set; }
        public Produtores Produtores { get; set; }
        public bool Principal { get; set; }
        public Int64 ClientesID { get; set; }
        public Clientes Clientes { get; set; }
    }

}
