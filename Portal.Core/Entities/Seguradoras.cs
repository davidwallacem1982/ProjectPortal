using System;
using System.Collections.Generic;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class Seguradoras : BaseCadastros
    {
        public virtual ICollection<Apolices> Apolices { get; set; }
        public virtual ICollection<Sinistros> Sinistros { get; set; }
        public virtual ICollection<Rel_Seguradoras_Clientes_Produtos> Rel_Seguradoras_Clientes_Produtos { get; set; }
        //Apelido da Seguradora
        public string Apelido { get; set; }
    }

}
