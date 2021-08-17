using System;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class Contatos : BaseEntity
    {
        public Int64 ClientesID { get; set; }
        public Int64 UnidadesID { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public string Cargo { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Observacao { get; set; }
        public bool Habilitado { get; set; }
    }

}
