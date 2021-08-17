using System;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class Feriados : BaseEntity
    {
        public DateTime DataFeriado { get; set; }
        public string Descricao { get; set; }
    }

}
