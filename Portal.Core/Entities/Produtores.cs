using System;
using System.Collections.Generic;

namespace Portal.Core.Entities
{

    [Serializable()]
    public class Produtores : BaseEntity
    {
        public string Nome { get; set; }
        public string Email { get; set; }

        public Rel_Usuarios_Produtores Rel_Usuarios_Produtores { get; set; }

        public virtual ICollection<Produtores_Clientes> Produtores_Clientes { get; set; }

        public virtual ICollection<Rel_Produtores_Clientes_Produtos> Rel_Produtores_Clientes_Produtos { get; set; }

        //public virtual ICollection<Rel_Usuarios_Produtores> Rel_Usuarios_Produtores { get; set; }
    }

}
