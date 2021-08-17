using System;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class Veiculos_Consultas : BaseConsulta
    {
        public Int64 VeiculosID { get; set; }
        public Veiculos Veiculos { get; set; }
    }

}
