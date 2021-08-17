using System;
using System.Collections.Generic;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class Controllers : BaseEntity
    {
        public string Controller { get; set; }
        public bool Habilitado { get; set; }
        public virtual ICollection<Methods> methods { get; set; }
    }

}
