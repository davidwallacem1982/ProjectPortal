using System;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class Transportadores_Veiculos : BaseEntity
    {
        public Int64 TransportadoresID { get; set; }
        public Transportadores Transportadores { get; set; }
        public Int64 VeiculosID { get; set; }
        public Veiculos Veiculos { get; set; }
    }

}
