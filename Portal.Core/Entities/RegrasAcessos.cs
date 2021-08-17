using System;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class RegrasAcessos : BaseEntity
    {
        public Int64? HttpMethodsID { get; set; }
        public string Controller { get; set; }
        public string Metodo { get; set; }
        public string Roles { get; set; }
        public string HttpMethod { get; set; }
        public bool Habilitado { get; set; }

        public HttpMethods HttpMethods { get; set; }

        public Methods Methods { get; set; }

        public Controllers Controllers { get; set; }
    }

}
