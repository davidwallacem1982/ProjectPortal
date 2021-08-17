using System;
using System.Collections.Generic;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class Methods : BaseEntity
    {
        public Int64 ControllersID { get; set; }
        public Controllers Controllers { get; set; }

        public string Method { get; set; }

        public bool Habilitado { get; set; }

        public virtual ICollection<HttpMethods> httpMethods { get; set; }
    }

}
