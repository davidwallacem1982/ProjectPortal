using System.Collections.Generic;

namespace Portal.Core.Entities
{
    public class Reguladoras : BaseCadastros
    {
        public virtual ICollection<Sinistros> Sinistros { get; set; }
    }
}
