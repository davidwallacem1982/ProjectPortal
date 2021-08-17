using System.Collections.Generic;

namespace Portal.Core.Entities
{
    public class TiposAtendimentos : BaseTipos
    {
        public bool Bloqueante { get; set; }

        public virtual ICollection<Atendimentos> Atendimentos { get; set; }
    }
}
