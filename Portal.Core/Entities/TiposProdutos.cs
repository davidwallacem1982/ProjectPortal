using System;
using System.Collections.Generic;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class TiposProdutos : BaseTipos
    {
        public bool Seguro { get; set; }

        public virtual ICollection<Produtos> Produtos { get; set; }
    }

}
