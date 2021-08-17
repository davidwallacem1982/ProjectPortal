using System;

namespace Portal.Core.Models
{
    public class DocumentosSinistroViewModel
    {
        public long Id { get; set; }
        public string Titulo { get; set; }
        public string Tipo { get; set; }
        public Int64? Status { get; set; }
        public Int64 SinistroID { get; set; }
        public Int64? AtendimentoID { get; set; }
        public int Sequencial { get; set; }
        public string url { get; set; }
    }
}
