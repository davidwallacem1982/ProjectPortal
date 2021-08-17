using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Core.Entities
{
    public class Cargo
    {
        public long Id { get; set; }

        public string Nome { get; set; }

        public ICollection<ListaContatosPortal> ListaContatosPortal { get; set; }

    }
}
