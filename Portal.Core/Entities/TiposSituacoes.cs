namespace Portal.Core.Entities
{
    public class TiposSituacoes : BaseTipos
    {
        public bool VinculaData { get; set; }
        public string NomeSubstituto { get; set; }
        public bool ApareceDocumento { get; set; }
        public int Semaforo { get; set; }
    }

}
