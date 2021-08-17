using System;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class Ufs
    {
        public Int64 ID { get; set; }
        public int IBGE { get; set; }
        public string UF { get; set; }
    }

}
