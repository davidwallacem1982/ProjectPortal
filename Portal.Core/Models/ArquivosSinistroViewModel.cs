using System;

namespace Portal.Core.Models
{
    public class ArquivosSinistroViewModel
    {
        public string Titulo { get; set; }
        public string Tipo { get; set; }
        public string Extensao { get; set; }
        public DateTime DataHora { get; set; }
        public long ControllerID { get; set; }
        public long SinistrosID { get; set; }
        public string url { get; set; }
        public int Sequencial { get; set; }
        public int Substituido { get; set; }
        public string UserName { get; set; }
        public long Status { get; set; }
    }
}
