using System;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class Documentos_ATM : BaseEntity
    {
        public Int64 Documentos_ATM_LotesID { get; set; }
        public Documentos_ATM_Lotes Documentos_ATM_Lotes { get; set; }

        public int Identificador { get; set; } // 1: CPF; 2: CNPJ
        public string cpfcnpj { get; set; }
        public string filial { get; set; }
        public string serie { get; set; }
        public string numero { get; set; }
        public DateTime DataEmissao { get; set; }
        public int tipodocto { get; set; } // 1: Manifesto; 2: Conhecimento; 3-NF; 4: Ordem Carga; 5: Outros
        public int tipotransp { get; set; } // 1: Rodoviário; 2: Marítimo; 3: Fluvial; 4:Ferroviário; 5:Aéreo
        public string Ramo { get; set; }
        public string uforigem { get; set; }
        public string ufdestino { get; set; }
        public bool urbano { get; set; }

        // Viagem Complementar
        public bool transcomplementar { get; set; }
        public int? tipotranscomplementar { get; set; } // 1: Rodoviário; 2: Marítimo; 3: Fluvial; 4:Ferroviário; 5:Aéreo
        public string uforigemcomplementar { get; set; }
        public string ufdestinocomplementar { get; set; }

        // Viagem Internacional
        public bool viageminternacional { get; set; }
        public int? tipoviageminternacional { get; set; } // 1:Importação; 2:Exportação
        public string paisviageminternacional { get; set; }
        public bool? taxarcfdcinternacional { get; set; }

        public string placa { get; set; }
        public decimal valormercadoria { get; set; }
        public int tipomercadoria { get; set; } // 1: Geral; 2:Específica
        public string cpfmotorista { get; set; }
        public string rgmotorista { get; set; }
        public string codigodeliberacaomotorista { get; set; }
        public bool taxaocd { get; set; }
        public bool taxaic { get; set; }
        public bool taxari { get; set; }
        public bool transpproprio { get; set; }
        public int tipomovimento { get; set; } // 1: Padrão; 2:Cancelado; 3:Cortesia; 4:Resp.Terceiros;5:CTe
        public string caixapostalatm { get; set; }
        public decimal? valorcontainer { get; set; }
        public decimal? valoracessorios { get; set; }
        public bool meiosproprios { get; set; }
        public bool rastreado { get; set; }
        public bool escolta { get; set; }
        public string obs { get; set; }
        public decimal? valorfrete { get; set; }
        public decimal? valordespesas { get; set; }
        public decimal? valorlucrosesperados { get; set; }
        public decimal? valorimpostos { get; set; }
        public DateTime dataembarque { get; set; }
        public decimal? valoravarias { get; set; }
        public bool mercadorianova { get; set; }
        public string protocolo { get; set; }
        public string codigodanfedacte { get; set; }
        public string codigocancelamento { get; set; }
        public DateTime? datacancelamentosefaz { get; set; }
        public DateTime? datacancelamentoatm { get; set; }

        // DDR
        public int? identificadorddr { get; set; } // 1: CPF; 2: CNPJ
        public string cpfcnpjddr { get; set; }
        public string cnpjemissor { get; set; }
        public int? tipoddr { get; set; } // 1: Parcial; 2: Participação; 3: Total
        public decimal? valormercadoriaoriginal { get; set; }

        // CTe
        public string cnpjtomador { get; set; }
        public string cnpjremetente { get; set; }
        public string cnpjexpedidor { get; set; }
        public string cnpjdestinatario { get; set; }
        public string cnpjrecebedor { get; set; }
        public string ceporigem { get; set; }
        public string cepdestino { get; set; }
        public string municipioorigem { get; set; }
        public string municipiodestino { get; set; }
        public DateTime? dataprotocolosefaz { get; set; }
        public string nomenavio { get; set; }
        public string nomebalsa { get; set; }
        public string codigoliberacaolimite { get; set; }
        public DateTime dataaverbacao { get; set; }
    }

}
