using Portal.Core.Notifications;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portal.Core.Models
{
    public class InfoSinistroViewModel : Notifica
    {
        public Int64 ID { get; set; }
        [Display(Name = "Cliente")]
        public string Cliente { get; set; }

        [Display(Name = "Data do Sinistro")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DataSinistro { get; set; }
        [Display(Name = "Ramo")]
        public string Ramos { get; set; }

        public int RamosSegurosID { get; set; }
        [Display(Name = "Seguradora")]
        public string Seguradora { get; set; }

        [Display(Name = "Reguladora")]
        public string Reguladora { get; set; }

        [Display(Name = "Natureza do Evento")]
        public string NaturezaSinistro { get; set; }

        public Int64 SeguradorasID { get; set; }

        [Display(Name = "Protocolo")]
        public Int64 Protocolo { get; set; }

        [Display(Name = "Tipos de Evento")]
        public string TiposSinistros { get; set; }

        [Display(Name = "Dados Estatíticos")]
        public string DadosEstatiticos { get; set; }

        [Display(Name = "Despesas do Sinistro")]
        public decimal? DespesasSinistro { get; set; }
        /// <summary>
        /// Valor da Carga ou Valor Embarcado
        /// </summary>
        [Display(Name = "Valor")]
        public decimal ValorCarga { get; set; }
        /// <summary>
        /// Valor da Indenização
        /// </summary>
        [Display(Name = "Estimativa/Valor Indenizado")]
        public decimal? ValorIndenizado { get; set; }
        /// <summary>
        /// Valor do Prejuizo
        /// </summary>
        [Display(Name = "Estimativa/Valor do Prejuízo")]
        public decimal? ValorSinistro { get; set; }

        [Display(Name = "Data da Indenização")]
        [DataType("datetime2"), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Column("DataIndenizacao", TypeName = "datetime2")]
        public DateTime? DataIndenizacao { get; set; }

        [Display(Name = "Último Status")]
        public string UltimoStatus { get; set; }

        [Display(Name = "Data do Aviso")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DataAviso { get; set; }

        [Display(Name = "Data da Conclusão")]
        [DataType("datetime2"), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Column("DataSinistro", TypeName = "datetime2")]
        public DateTime? DataConclusao { get; set; }

        [Display(Name = "Cidade Origem")]
        public string CidadeOrigem { get; set; }

        [Display(Name = "UF Origem")]
        public string UFOrigem { get; set; }

        [Display(Name = "Cidade Destino")]
        public string CidadeDestino { get; set; }

        [Display(Name = "UF Destino")]
        public string UFDestino { get; set; }

        [Display(Name = "Mercadoria")]
        public string Mecadoria { get; set; }

        [Display(Name = "Nome Motorista")]
        public string NomeMotorista { get; set; }
        [Display(Name = "Placa Cavalo")]
        public string PlacaVeiculo { get; set; }
        [Display(Name = "Placa Carreta")]
        public string PlacaCarreta { get; set; }

        [Display(Name = "Local do Envento")]
        public string LocalSinistro { get; set; }

        [Display(Name = "Cidade do Evento")]
        public string CidadeSinistro { get; set; }

        [Display(Name = "UF Evento")]
        public string UFSinistro { get; set; }

        [Display(Name = "Pendência Documental")]
        public bool Pendente { get; set; }
    }

}
