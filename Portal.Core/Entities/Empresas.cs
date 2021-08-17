using System.Collections.Generic;

namespace Portal.Core.Entities
{
    public class Empresas : BaseCadastros
    {
        public virtual ICollection<Cliente_Produtos> Cliente_Produtos { get; set; }
    }

}
