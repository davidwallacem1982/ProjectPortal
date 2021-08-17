using System;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class ExcecaoRegrasAlertaEmails : BaseEntity
    {
        public Int64 RegrasEnvioEmailsID { get; set; }
        public string Referencia { get; set; }
        public string ReferenciaID { get; set; }
    }

}
