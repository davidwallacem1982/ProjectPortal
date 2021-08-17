using System;

namespace Portal.Core.Entities
{
    public class Rel_Usuarios_Produtores : BaseEntity
    {
        public string UsuariosID { get; set; }
        public Int64 ProdutoresID { get; set; }
        public Produtores Produtores { get; set; }
    }
}
