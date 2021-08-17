using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portal.Core.Notifications
{
    public class Notifica
    {
        public Notifica() => Notifications = new List<Notifica>();

        [NotMapped]
        public string NomePropriedade { get; set; }

        [NotMapped]
        public string Mensagem { get; set; }

        [NotMapped]
        public List<Notifica> Notifications;
    }
}
