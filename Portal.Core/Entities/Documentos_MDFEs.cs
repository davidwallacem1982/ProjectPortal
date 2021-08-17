using System;
using System.Collections.Generic;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class Documentos_MDFEs : BaseDocumentos
    {
        public virtual ICollection<rel_CTs_MDFEs> rel_CTs_MDFEs { get; set; }
        public virtual ICollection<rel_NFs_MDFEs> rel_NFs_MDFEs { get; set; }
    }

}
