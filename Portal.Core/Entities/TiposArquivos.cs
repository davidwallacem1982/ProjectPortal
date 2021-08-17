using System;
using System.Collections.Generic;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class TiposArquivos : BaseEntity
    {
        public string Tipo { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<CheckLists> CheckLists { get; set; }
    }

}
