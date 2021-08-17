using System;
using System.Collections.Generic;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class Veiculos : BaseEntity
    {
        public string Placa { get; set; }
        public string Renavam { get; set; }
        public string tpProp { get; set; }  // Proprietário do Veículo (P: Próprio; T: Terceiro)
        public string tpVeic { get; set; }  // Tipo do Veículo: 0: Tração; 1: Reboque
        public string tpRodado { get; set; } // Tipo de Rodado
        public string tpCar { get; set; } // Tipo Carroceria
        public string UF { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string CapacidadeKG { get; set; }
        public string CapacidadeM3 { get; set; }
        public string Cor { get; set; }
        public int? anoFabricacao { get; set; }
        // Proprietario
        public string propDocumento { get; set; }
        public string propRNTRC { get; set; }
        public string propNome { get; set; }
        public string propIE { get; set; }
        public string propUF { get; set; }
        public string propTpProp { get; set; }
        public virtual ICollection<Veiculos_Consultas> Veiculos_Consultas { get; set; }
        public virtual ICollection<Transportadores_Veiculos> Transportadores_Veiculos { get; set; }
    }

}
