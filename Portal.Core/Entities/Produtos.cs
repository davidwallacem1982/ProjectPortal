using System;
using System.Collections.Generic;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class Produtos : BaseEntity
    {
        public int TiposProdutosID { get; set; }
        public TiposProdutos TiposProdutos { get; set; }
        public string Descricao { get; set; }
        public bool LancamentoVariavel { get; set; }
        public string Observacao { get; set; }
        public bool Habilitado { get; set; }
        public virtual ICollection<Cliente_Produtos> Clientes_Produtos { get; set; }
    }

}
