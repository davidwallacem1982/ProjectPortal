namespace Portal.Core.Models
{
    public class ArquivosViewModel
    {
        public long ID { get; set; }
        public string Titulo { get; set; }
        public string Tipo { get; set; }
        public string Extensao { get; set; }
        public string url { get; set; }
        public long AtendimentoID { get; set; }
        public string Status { get; set; }
        public string Motivo { get; set; }
        public int Sequencial { get; set; }
    }
}
