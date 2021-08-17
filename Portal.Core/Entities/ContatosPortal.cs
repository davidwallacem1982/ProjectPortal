using System;
using System.Collections.Generic;

namespace Portal.Core.Entities
{
    public class ContatosPortal
    {
        public long Id { get; set; }

        public string Titulo { get; set; }

        public ICollection<ListaContatosPortal> ListaContatosPortal { get; set; }

    }
}
