using System;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class Criticidades : BaseEntity
    {
        public string Descricao { get; set; }
        public string Prioridade { get; set; }
    }

}
