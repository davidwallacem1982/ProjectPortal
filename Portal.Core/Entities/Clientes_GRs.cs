using System;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class Clientes_GRs : BaseEntity
    {
        public Int64 ClientesID { get; set; }
        public Clientes Clientes { get; set; }
        public Int64 GRsID { get; set; }
        public GRs GRs { get; set; }
        public int Tipo { get; set; } // 0: Gerenciamento; 1: Cadastro & Consulta; 2: CFTV
    }

}
