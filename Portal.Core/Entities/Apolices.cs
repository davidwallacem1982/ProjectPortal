using System;
using System.Collections.Generic;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class Apolices : BaseEntity
    {
        public Int64 UnidadesID { get; set; }
        public Unidades Unidades { get; set; }
        public Int64 SeguradorasID { get; set; }
        public Seguradoras Seguradoras { get; set; }
        public int RamosSegurosID { get; set; }
        public DateTime InicioVigencia { get; set; }
        public DateTime FinalVigencia { get; set; }
        public decimal LMG { get; set; }
        public decimal? ValorContainer { get; set; }
        public decimal? ValorAcessorio { get; set; }
        public bool Avarias { get; set; }
        public bool Habilitado { get; set; }
        public virtual ICollection<DDRs> DDRs { get; set; }
    }

}
