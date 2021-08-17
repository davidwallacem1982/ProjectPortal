using System;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class Campos : BaseEntity
    {
        public Int64 TabelasID { get; set; }
        public Tabelas Clientes { get; set; }
        public string NomeCampo { get; set; }
        public bool Insert { get; set; }
        public bool Update { get; set; }
    }

}
