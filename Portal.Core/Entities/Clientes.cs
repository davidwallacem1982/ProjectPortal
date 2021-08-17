using System;
using System.Collections.Generic;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class Clientes : BaseCadastros
    {
        public string Ambiente { get; set; }
        public Int64? OperacionaisID { get; set; }

        // Configurações para Histórico Motorista/Veículo
        public int? IntervaloPesquisa { get; set; }
        public int? QuantidadeMinimaViagens { get; set; }

        // Configurações para Identificação dos Sinistros
        public int? IntervaloSinistros { get; set; }
        public decimal? PercentualMaximoSinistros { get; set; }
        public decimal? PercentualValorMaximoPrejuizo { get; set; }

        // Tipo de averbação
        public int? TipoAverbacoesID { get; set; }
        public string Observacao { get; set; }

        public Produtores_Clientes Produtores_Clientes { get; set; }

        // Cadastros
        public virtual ICollection<Unidades> Unidades { get; set; }

        // Documentos
        public virtual ICollection<Documentos_CTEs> Documentos_CTEs { get; set; }
        public virtual ICollection<Documentos_MDFEs> Documentos_MDFEs { get; set; }
        public virtual ICollection<Documentos_NFEs> Documentos_NFEs { get; set; }

        // Verificações de GR
        public virtual ICollection<Verificacoes> Verificacoes { get; set; }

        // Cadastro & Consulta
        public virtual ICollection<Motoristas_Consultas> Motoristas_Consultas { get; set; }
        public virtual ICollection<Veiculos_Consultas> Veiculos_Consultas { get; set; }

        // Produtores
        //public virtual ICollection<Produtores_Clientes> Produtores_Clientes { get; set; }

        // Gerenciadora
        public virtual ICollection<Clientes_GRs> Clientes_GRs { get; set; }
        public virtual ICollection<Contatos> Contatos { get; set; }
    }
}