using System;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class Verificacoes : BaseEntity
    {
        public Int64 ClientesID { get; set; }
        public Clientes Clientes { get; set; }
        public DateTime DataInclusao { get; set; }
        public string Filter { get; set; }
        public int TipoFilter { get; set; }
        public int Status { get; set; }
        public string userName { get; set; }
    }

}
