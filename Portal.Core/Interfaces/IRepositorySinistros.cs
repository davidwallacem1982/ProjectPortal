using Portal.Core.Entities;
using Portal.Core.Models;
using System.Collections.Generic;

namespace Portal.Core.Interfaces
{
    public interface IRepositorySinistros : IRepository<Sinistros>
    {
        List<SinistrosAllViewModel> SelectSinistrosAll();

        List<SinistrosViewModel> SelectSinistrosClienteByClienteId(long clienteId);

        List<SinistrosProdutorViewModel> SelectSinistrosProdutorByusuariosID(string usuariosID);

        int SelectByTiposSinistrosID(long sinistrosID);

        InfoSinistroViewModel SelectInfoSinistroById(long Id);

        List<GraficoPizzaSeguradoViewModel> SelectGraficoPizzaSeguradoByclienteId(long clienteId);

        List<GraficoColunaSeguradoViewModel> SelectGraficoColunaLinhaSeguradoByclienteId(long clienteId);

        List<GraficoColunaSeguradoAnoViewModel> SelectGraficoColunaLinhaSeguradoByclienteIdAno(long clienteId, string ano);

        List<GraficoColunaSeguradoRamoViewModel> SelectGraficoColunaLinhaSeguradoByclienteIdRamo(long clienteId, string ramo);

        List<GraficoColunaSeguradoRamoAnoViewModel> SelectGraficoColunaLinhaSeguradoByclienteIdRamoAno(long clienteId, string ramo, string ano);

        List<GraficoPizzaProdutorViewModel> SelectGraficoPizzaProdutorByusuarioId(string usuarioId);

        List<GraficoColunaProdutorViewModel> SelectGraficoColunaLinhaProdutorByusuarioId(string usuarioId);

        List<GraficoColunaProdutorRamoViewModel> SelectGraficoColunaLinhaProdutorByusuarioIdRamo(string usuarioId, string ramo);

        List<GraficoColunaProdutorRamoAnoViewModel> SelectGraficoColunaLinhaProdutorByusuarioIdRamoAno(string usuarioId, string ramo, string ano);

        List<GraficoPizzaAdministradorViewModel> SelectGraficoPizzaAdministrador();

        List<GraficoColunaAdministradorViewModel> SelectGraficoColunaLinhaAdministrador();

        List<GraficoColunaAdministradorAnoViewModel> SelectGraficoColunaLinhaAdministradorByAno(string ano);

        List<GraficoColunaAdministradorRamoViewModel> SelectGraficoColunaLinhaAdministradorByRamo(string ramo);

        List<GraficoColunaAdministradorRamoAnoViewModel> SelectGraficoColunaLinhaAdministradorByRamoAno(string ramo, string ano);


        List<GraficoColunaProdutorAnoViewModel> SelectGraficoColunaLinhaProdutorByAno(string usuarioId, string ano);


    }
}
