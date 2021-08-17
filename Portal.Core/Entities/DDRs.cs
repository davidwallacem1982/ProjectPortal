using System;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class DDRs : BaseEntity
    {
        public Int64 ApolicesID { get; set; }
        public Apolices Apolices { get; set; }
        public string CNPJ { get; set; }
        public DateTime? Vigencia { get; set; }
        public bool Total { get; set; } // RCF-DC + RCTR-C
        public bool Parcial { get; set; } // RCF-DC
    }

}
