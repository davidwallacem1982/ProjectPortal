using System;

namespace Portal.Core.Entities
{
    public class EmailsClientes : BaseEntity
    {
        public Int64? ClientesID { get; set; }
        public string Email { get; set; }
        public bool IsSinistro { get; set; }
    }

}
