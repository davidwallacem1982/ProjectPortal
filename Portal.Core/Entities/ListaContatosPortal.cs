using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Core.Entities
{
    public class ListaContatosPortal
    {
        public long Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Telefone { get; set; }

        public string Site { get; set; }

        public long ContatosPortalId { get; set; }

        public ContatosPortal ContatosPortal;

        public long CargoId { get; set; }


        public Cargo Cargo;

    }
}
