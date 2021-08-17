using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class Atendimentos : BaseEntity
    {
        public Int64 ClientesID { get; set; }
        public string PlacaVeiculo { get; set; }
        // Motorista
        public string NomeMotorista { get; set; }
        // Regras do GR
        public bool CadastroConsulta { get; set; } // 0: Pendente; 1: ok;
        public Int64? MonitoradoGRSID { get; set; }
        public bool MonitoradoGR { get; set; } // 0: Não; 1: Sim;
        public Int64? RastreadoGRSID { get; set; }
        public bool Rastreado { get; set; } // 0: Não; 1: Sim;
        public short? TipoRastreador { get; set; } // 0: Autotrac; 1: ;(corp)
        public short? TipoRedundancia { get; set; } // 0: Autotrac; 1: ;(corp)
        public bool Escolta { get; set; } // 0: Não; 1: Sim;
        public Int64? UnidadesID { get; set; }
        public Unidades Unidades { get; set; }
        public string Criticidade { get; set; }
        // Seguradora
        public Int64 SeguradorasID { get; set; }
        // Reguladora
        public Int64 ReguladorasID { get; set; }
        public Reguladoras Reguladoras { get; set; }
        public int TiposNaturezaSinistrosID { get; set; }
        public DateTime DataAtendimento { get; set; }
        public DateTime DataAviso { get; set; }
        public Int64 Protocolo { get; set; }
        public string CidadeAtendimento { get; set; }
        public string UFAtendimento { get; set; }
        public string LocalAtendimento { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string CidadeOrigem { get; set; }
        public string UFOrigem { get; set; }
        public string CidadeDestino { get; set; }
        public string UFDestino { get; set; }
        public string Mecadoria { get; set; }
        public bool ImpactoAmbiental { get; set; } // 0: Não; 1: Sim;
        public decimal ValorCarga { get; set; } // valor total transportado
        public decimal ValorPrejuizo { get; set; } // valor do Atendimento total da carga ou valor parcial
        public int? TiposFretesID { get; set; } // 0: FOB; 1: CIF;
        public bool PistaDupla { get; set; } // 0: Pista Simples; 1: Pista Dupla
        public bool Acostamento { get; set; } // 0: Com Acostamento; 1: Sem Acostamento
        public int? UfID { get; set; }
        public int? CidadeID { get; set; }
        public Int64? SinistrosID { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public DateTime? DataConclusao { get; set; }
        [StringLength(1500)]
        public string MotivoAtendimento { get; set; }
        public string Situacao { get; set; } // Pago/FALTA ND
        [StringLength(500)]
        public string Observacao { get; set; }
        [StringLength(500)]
        public string Historico { get; set; }
        //public Int64? ArquivosID { get; set; }
        public DateTime? UltimoDocumentoReguladora { get; set; }
        public DateTime? RelatorioFinal { get; set; }
        public DateTime? AnaliseSeguradora { get; set; }
        public DateTime? envioDocumentosSeg { get; set; }
        public DateTime? RecebimentoAvisoCIA { get; set; }
        public virtual ICollection<Documentos_Atendimentos> DocumentosAtendimentos { get; set; }
        public virtual ICollection<Arquivos> Arquivos { get; set; }
    }
}
