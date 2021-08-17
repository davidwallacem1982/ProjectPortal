namespace Portal.Core.Models
{
    public class GraficoColunaProdutorRamoViewModel
    {
        /// <summary>
        /// Quantidade de Eventos(Sinistros) por ano
        /// </summary>
        public int EventosAno { get; set; }
        /// <summary>
        /// Total de Eventos(Sinistros) do Segurado
        /// </summary>
        public int TotalEventos { get; set; }
        /// <summary>
        /// Ano da Data do Sinistro
        /// </summary>
        public string Ano { get; set; }
        /// <summary>
        /// Ramo
        /// </summary>
        public string Ramo { get; set; }
        /// <summary>
        /// Valor do Prejuízo(ValorSinistro) por Ano
        /// </summary>
        public decimal ValorPrejuizoAno { get; set; }
    }
}
