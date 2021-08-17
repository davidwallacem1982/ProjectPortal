using System;
using System.Collections.Generic;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class HttpMethods : BaseEntity
    {
        public Int64 MethodsID { get; set; }
        public Methods Methods { get; set; }
        public string HttpMethod { get; set; }
        public string Descricao { get; set; }
        public bool Habilitado { get; set; }
        public virtual ICollection<RegrasEnvioEmails> regrasEnvioEmails { get; set; }
        //public virtual ICollection<RegrasAcessos> Sinistros { get; set; }
    }

}
