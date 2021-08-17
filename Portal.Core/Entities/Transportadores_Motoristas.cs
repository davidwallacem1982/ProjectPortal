using System;

namespace Portal.Core.Entities
{

    [Serializable()]
    public class Transportadores_Motoristas : BaseEntity
    {
        public Int64 TransportadoresID { get; set; }
        public Transportadores Transportadores { get; set; }
        public Int64 MotoristasID { get; set; }
        public Motoristas Motoristas { get; set; }
    }

}
