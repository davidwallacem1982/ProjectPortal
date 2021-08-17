using Portal.Core.Notifications;

namespace Portal.Core.Entities
{    /// <summary>
     /// Classe BaseEntity criada para as Entidades que contém as mesmas propriedades 
     /// e ela herda da classe Notifica
     /// </summary>
    public class BaseEntity : Notifica
    {
        public virtual long Id { get; set; }
    }
}
