using System;
using System.Collections.Generic;

namespace Portal.Core.Entities
{
    [Serializable()]
    public class Cliente_Produtos : BaseEntity
    {
        public Int64 UnidadesID { get; set; } // ok
        public Unidades Unidades { get; set; }
        public Int64 ProdutosID { get; set; } // ok
        public Produtos Produtos { get; set; }
        public Int64 EmpresasID { get; set; } // ok
        public Empresas Empresas { get; set; }
        public bool? SeguroNovo { get; set; } // ok
        public string Apolice { get; set; } // ok
        public int? Quantidade { get; set; } // ok
        public decimal? Valor { get; set; } // ok
        public int? DiasPagamento { get; set; } // ok
        public decimal? Imposto { get; set; } // ok
        public decimal? Acrescimo { get; set; } // ok
        public decimal? Desconto { get; set; } // ok
        public decimal? ValorTotal { get; set; } // ok
        public int? QuantidadeParcelas { get; set; } // ok
        public DateTime? Inicio { get; set; } // ok
        public DateTime? Fim { get; set; } // ok
        public DateTime? Emissao { get; set; } // ok
        public int Parcelas { get; set; } // ok
        public decimal? PremioMinimo { get; set; } // ok
        public decimal? ValorMaximoViagem { get; set; } // ok
        public bool Habilitado { get; set; } // ok
        public Int64? TipoAverbacaoID { get; set; } // ok
        public Int64? TipoRegraAverbacaoID { get; set; } // ok
        public Int64? EmpresasAverbacaoID { get; set; } // ok
        public DateTime? DtDesabilitado { get; set; } // ok
        public DateTime? DtCancelamento { get; set; }
        public Int64? TipoAlteraçãoID { get; set; }
        public Int64? TipoMotivoID { get; set; }
        public string Observacao { get; set; }
        public virtual ICollection<Rel_Produtores_Clientes_Produtos> Rel_Produtores_Clientes_Produtos { get; set; }
        public virtual ICollection<Rel_Seguradoras_Clientes_Produtos> Rel_Seguradoras_Clientes_Produtos { get; set; }
        public virtual ICollection<Faturamentos> faturamentos { get; set; }
    }

}
