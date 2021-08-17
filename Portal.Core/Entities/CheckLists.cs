using System;

namespace Portal.Core.Entities
{
    public class CheckLists : BaseEntity
    {
        public Int64 RamosID { get; set; }
        public Int64 TiposID { get; set; }
        public TiposArquivos TiposArquivos { get; set; }
        public Int64 SeguradorasID { get; set; }
        public int Status { get; set; }
        public int DePara { get; set; }
        public Int64 SinistrosID { get; set; }
        public Sinistros Sinistros { get; set; }
        public int Ativo { get; set; }
        public int Padrao { get; set; }
        public Int64? TiposSinistrosID { get; set; }
    }
}
