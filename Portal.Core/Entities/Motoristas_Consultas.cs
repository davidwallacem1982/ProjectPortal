using System;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class Motoristas_Consultas : BaseConsulta
    {
        // Motoristas
        public Int64 MotoristasID { get; set; }
        public Motoristas Motoristas { get; set; }
    }

}
