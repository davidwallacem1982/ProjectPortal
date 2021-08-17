using System;
using System.Collections.Generic;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class RamosSeguros : BaseTipos
    {
        public virtual ICollection<Sinistros> Sinistros { get; set; }
    }
}
