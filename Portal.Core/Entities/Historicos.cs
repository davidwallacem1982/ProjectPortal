using System;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class Historicos : BaseEntity
    {
        public string usuario { get; set; }
        public DateTime dataHistorico { get; set; }
        public Int64 controllerID { get; set; }
        public string descricao { get; set; }
        public Int64 protocolo { get; set; }
        public Int64 protocoloAtenSin { get; set; }
    }

}
