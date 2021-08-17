using System.Collections.Generic;

namespace Portal.Core.Models
{
    public class ContatosPortalViewModel
    {
        public long Id { get; set; }

        public string Titulo { get; set; }

        public List< ListaContatosViewModel > Itens { get; set; }
    }
}
