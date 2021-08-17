using System;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class Cidades : BaseEntity
    {
        public int IBGE_UF { get; set; }
        public string Cidade { get; set; }
    }

}
