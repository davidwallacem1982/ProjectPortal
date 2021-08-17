using System;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class Operacionais : BaseEntity
    {
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}
