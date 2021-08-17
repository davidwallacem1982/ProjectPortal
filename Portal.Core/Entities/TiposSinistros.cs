using System.Collections.Generic;

namespace Portal.Core.Entities
{
    public class TiposSinistros
    {
        public int Id { get; set; }
        public bool Bloqueante { get; set; }
        public string Tipo { get; set; }
        public string DePara { get; set; }
        public bool Habilitado { get; set; }
        public virtual ICollection<Sinistros> Sinistros { get; set; }
    }
}
