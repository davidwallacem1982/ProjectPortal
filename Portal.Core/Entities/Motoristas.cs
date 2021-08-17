using System;
using System.Collections.Generic;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class Motoristas : BaseCadastros
    {
        // RG
        public string RG { get; set; }
        public string OrigemRG { get; set; }
        public DateTime? DataEmissaoRG { get; set; }
        // Dados Pessoais
        public DateTime? Nascimento { get; set; }
        public string Filiacao { get; set; }
        public string TelefoneCelular { get; set; }

        // CNH
        public string numeroCNH { get; set; }
        public DateTime? VencimentoCNH { get; set; }
        public string OrigemCNH { get; set; }
        public string tipoCNH { get; set; }

        public virtual ICollection<Motoristas_Consultas> Motoristas_Consultas { get; set; }
        public virtual ICollection<Transportadores_Motoristas> Transportadores_Motoristas { get; set; }
    }

}
