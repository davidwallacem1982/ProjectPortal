using System;

namespace Portal.Core.Entities
{
    public class BaseConsulta : BaseEntity
    {
        public Int64 GRsID { get; set; }
        public GRs GRs { get; set; }
        public Int64 ClientesID { get; set; }
        public Clientes Clientes { get; set; }
        public Int64 TransportadoresID { get; set; }
        public Transportadores Transportadores { get; set; }
        public DateTime dataInclusao { get; set; }
        public DateTime? dataPesquisa { get; set; }
        public string UserName { get; set; }
        public bool perfilSecuritario { get; set; }
        public string chaveLiberacao { get; set; }
        public DateTime? Validade { get; set; }
    }
}
