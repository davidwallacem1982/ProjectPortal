using System;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class TiposSituacoesUsuarios
    {
        public int ID { get; set; }
        public string ClienteID { get; set; }
        public int SituacaoID { get; set; }
        public bool Habilitado { get; set; }
        public bool VinculaData { get; set; }
    }

}
