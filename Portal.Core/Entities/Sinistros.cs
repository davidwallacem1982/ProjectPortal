using System;
using System.Collections.Generic;

namespace Portal.Core.Entities
{

    [Serializable()]
    public class Sinistros : BaseEntity
    {
        public int RamosSegurosID { get; set; }
        public RamosSeguros RamosSeguros { get; set; }
        // Veiculo
        public string PlacaVeiculo { get; set; }
        public string PlacaCarreta { get; set; }
        // Motorista
        public string CPFMotorista { get; set; }
        public int? TiposVinculosID { get; set; }
        public int? TiposTempoMotoristaID { get; set; }
        // transportador
        public Int64? TransportadoresID { get; set; }
        public Transportadores Transportadores { get; set; }
        // Regras do GR
        public bool CadastroConsulta { get; set; } // 0: Pendente; 1: ok;
        public Int64? MonitoradoGRSID { get; set; }
        public bool? MonitoradoGR { get; set; } // 0: Não; 1: Sim;
        public Int64? RastreadoGRSID { get; set; }
        public bool Rastreado { get; set; } // 0: Não; 1: Sim;
        public short? TipoRastreador { get; set; } // 0: Autotrac; 1: ;(corp)
        public short? TipoRedundancia { get; set; } // 0: Autotrac; 1: ;(corp)
        public bool Escolta { get; set; } // 0: Não; 1: Sim;
        public string EmpresaEscolta { get; set; }
        // Segurado/Cliente
        public Int64 UnidadesID { get; set; }
        public Unidades Unidades { get; set; }
        // Seguradora
        public Int64 SeguradorasID { get; set; }
        public Seguradoras Seguradoras { get; set; }
        // Reguladora
        public Int64? ReguladorasID { get; set; }
        public Reguladoras Reguladoras { get; set; }
        public int TiposSinistrosID { get; set; }
        public TiposSinistros TiposSinistros { get; set; }
        public CheckLists CheckLists { get; set; }
        public TiposNaturezaSinistros TiposNaturezaSinistros { get; set; }
        public int TiposNaturezaSinistrosID { get; set; }
        public DateTime DataSinistro { get; set; }
        public DateTime DataAviso { get; set; }
        public Int64 Protocolo { get; set; }
        public string Apolice { get; set; }
        public string Item { get; set; }
        public string CidadeSinistro { get; set; }
        public string UFSinistro { get; set; }
        public string RegiaoEvento { get; set; }
        public string LocalSinistro { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string CidadeOrigem { get; set; }
        public string UFOrigem { get; set; }
        public string CidadeDestino { get; set; }
        public string UFDestino { get; set; }
        public string Mecadoria { get; set; }
        public string Produtos { get; set; }
        // Vitimas
        public int? TiposSituacaosMotoristasID { get; set; }
        public bool ImpactoAmbiental { get; set; } // 0: Não; 1: Sim;
        public decimal ValorCarga { get; set; } // valor total transportado
        public decimal ValorSinistro { get; set; } // valor do sinistro total da carga ou valor parcial
        public decimal? ValorSalvado { get; set; }
        public decimal? ValorRessarcimento { get; set; }
        public DateTime? DataRecebimentoSalvado { get; set; }
        public decimal? ValorSubLimite { get; set; }
        public decimal DespesasSinistro { get; set; }
        public decimal ValorIndenizado { get; set; }
        public DateTime? DataIndenizacao { get; set; }
        public int? TiposFretesID { get; set; } // 0: FOB; 1: CIF;
        public int? TiposOperacoesID { get; set; }
        public int? TiposLotacoesID { get; set; } // 0: Fracionado; 1: Lotacao;
        public int? DistanciaViagemKM { get; set; }
        public int? DistanciaPedoridoKM { get; set; }
        public int? TiposPistasID { get; set; } // 0: RETA; 1: CURVA;
        public int? TiposTempoPistasID { get; set; } // 0: SECA; 1: MOLHADA
        public int? TiposConservacaoPistaID { get; set; }
        public bool PistaDupla { get; set; } // 0: Pista Simples; 1: Pista Dupla
        public int? TiposConcessionadaID { get; set; }
        public bool Acostamento { get; set; } // 0: Com Acostamento; 1: Sem Acostamento
        public int? TiposNivelID { get; set; }
        public int? TiposCulpabilidadeMotoristaID { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public DateTime? DataConclusao { get; set; }
        public DateTime? DataRecusao { get; set; }
        public string MotivoSinistro { get; set; }
        public string Situacao { get; set; } // Pago/FALTA ND
        public string Observacao { get; set; }
        public string Detalhes { get; set; }
        public DateTime? UltimoDocumentoReguladora { get; set; }
        public DateTime? RelatorioFinal { get; set; }
        public DateTime? AnaliseSeguradora { get; set; }
        public DateTime? EnvioDocumentosSeg { get; set; }
        public DateTime? RecebimentoAvisoCIA { get; set; }
        // Atendimentos
        public Int64? AtendimentoProtocolo { get; set; }
        public string Historico { get; set; }
        //public Int64? ArquivosID { get; set; }
        //public Arquivos Arquivos { get; set; }
        // Public Property SinistrosID As Nullable(Of Long)
        //public virtual ICollection<Documentos_Sinistros> DocumentosSinistros { get; set; }
        //public Ufs Ufs { get; set; }
        public virtual ICollection<Arquivos> Arquivos { get; set; }
        public string Analista { get; set; }
        public DateTime? DataAprovacaoIndenizacao { get; set; }
    }

}
