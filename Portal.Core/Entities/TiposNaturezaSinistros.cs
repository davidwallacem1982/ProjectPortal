using System;
using System.Collections.Generic;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class TiposNaturezaSinistros : BaseTipos
    {
        public virtual ICollection<Sinistros> Sinistros { get; set; }
    }

}
