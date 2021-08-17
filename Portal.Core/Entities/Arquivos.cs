using System;

namespace Portal.Core.Entities
{
    public class Arquivos : BaseEntity
    {
        public string Titulo { get; set; }
        public string Tipo { get; set; }
        public string Extensao { get; set; }
        public DateTime DataHora { get; set; }
        public Int64 AtendimentoID { get; set; }
        public Atendimentos Atendimentos { get; set; }
        public Int64 SinistroID { get; set; }
        public Sinistros Sinistros { get; set; }
        public Int64 ControllerID { get; set; }
        public string url { get; set; }
        public string UserName { get; set; }
        public Int64? Status { get; set; }
        public string UserNameStatus { get; set; }
        public string Motivo { get; set; }
        public int Sequencial { get; set; }
        public int Substituido { get; set; }
    }
}
