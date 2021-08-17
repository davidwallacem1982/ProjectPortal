using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Portal.Api.Interfaces;
using Portal.Core.Models;
using Portal.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Portal.TestesController
{
    public class Testes_SinistrosController
    {
        /* ============== Montagem de Cenário ============== */
        #region Start

        bool resultado = false;
        private readonly ITestOutputHelper output;
        private readonly Mock<IAppSinistros> mockAppSin;
        private readonly SinistrosController controller;


        /// <summary>
        /// Construtor Testes_SinistrosController
        /// </summary>
        public Testes_SinistrosController(ITestOutputHelper output)
        {
            this.output = output;
            mockAppSin = new Mock<IAppSinistros>();
            controller = new SinistrosController(mockAppSin.Object);
        }

        #endregion

        #region Métodos Testes da SinistrosController

        /// <summary>
        /// Teste do método ListSinistrosClienteByclienteId da SinistrosController que retorna um Json
        /// </summary>
        /// <param name="count">Total de Registro que do retorno</param>
        /// <param name="sinistroID">Id do Sinistro</param>
        /// <param name="seguradora">Nome da Seguradora</param>
        /// <param name="UltimoStatus">Status do Sinistro</param>
        [Theory]
        [MemberData(nameof(ListarSinistrosClienteData))]
        public void Json_ReturnsListarSinistrosCliente(int count, long sinistroID, string seguradora, string UltimoStatus)
        {
            // Arrange
            var mockSession = new MockHttpSession();
            var mockHttpContext = new Mock<HttpContext>();
            var mockControllerContext = new Mock<ControllerContext>();
            mockHttpContext.Setup(c => c.Session).Returns(mockSession);

            mockAppSin.Setup(repo => repo.GetListSinistrosClienteByclienteId(It.IsAny<long>())).Returns(GetSinistrosCliente());

            controller.ControllerContext = mockControllerContext.Object;
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var result = controller.ListarSinistrosCliente();

            // Assert
            var viewResult = Assert.IsType<JsonResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<SinistrosViewModel>>(viewResult.Value);
            Assert.Equal(count, model.Count());
            foreach (var item in model)
            {
                if (item.ID.Equals(sinistroID))
                {
                    if (item.Seguradora.Equals(seguradora))
                    {
                        if (item.UltimoStatus.Equals(UltimoStatus))
                        {
                            resultado = true;
                            break;
                        }
                    }
                }
            }
            Assert.True(resultado);
        }

        /// <summary>
        /// Teste do método ListSinistrosClienteByclienteId da SinistrosController que retorna um Json
        /// </summary>
        /// <param name="count">Total de Registro que do retorno</param>
        /// <param name="sinistroID">Id do Sinistro</param>
        /// <param name="seguradora">Nome da Seguradora</param>
        /// <param name="ramo">Tipo de Ramo</param>
        [Theory]
        [MemberData(nameof(ListarSinistrosProdutorData))]
        public void Json_ReturnsListarSinistrosProdutor(int count, long sinistroID, string seguradora, string ramo)
        {
            // Arrange
            var mockSession = new MockHttpSession();
            var mockHttpContext = new Mock<HttpContext>();
            var mockControllerContext = new Mock<ControllerContext>();

            mockHttpContext.Setup(c => c.Session).Returns(mockSession);

            mockAppSin.Setup(repo => repo.GetListSinistrosProdutorByusuarioID(It.IsAny<string>())).Returns(GetSinistrosProdutor());

            controller.ControllerContext = mockControllerContext.Object;
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var result = controller.ListarSinistrosProdutor();

            // Assert
            var viewResult = Assert.IsType<JsonResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<SinistrosProdutorViewModel>>(viewResult.Value);
            Assert.Equal(count, model.Count());
            foreach (var item in model)
            {
                if (item.ID.Equals(sinistroID))
                {
                    if (item.Segurado.Equals(seguradora))
                    {
                        if (item.Ramo.Equals(ramo))
                        {
                            resultado = true;
                            break;
                        }
                    }
                }
            }
            Assert.True(resultado);
        }

        /// <summary>
        /// Teste do VisualizarSinistro da SinistrosController
        /// </summary>
        /// <param name="sinistroID">Id do Sinistro</param>
        /// <param name="ufOrigem">Estado de Origem</param>
        /// <param name="ufDestino">Estado de Destito</param>
        /// <param name="ufSinistro">Estado de Sinistro</param>
        /// <param name="motorista">Nome do Motorista</param>
        [Theory]
        [MemberData(nameof(GetInfoSinistroByIdData))]
        public void VisualizarSinistro_ReturnsGetInfoSinistroById(long sinistroID, string ufOrigem, string ufDestino, string ufSinistro, string motorista)
        {
            // Arrange
            var mockSession = new MockHttpSession();
            var mockHttpContext = new Mock<HttpContext>();
            var mockControllerContext = new Mock<ControllerContext>();

            mockHttpContext.Setup(c => c.Session).Returns(mockSession);

            var mockAppSin = new Mock<IAppSinistros>();

            mockAppSin.Setup(repo => repo.GetInfoSinistroById(It.IsAny<long>())).Returns(GetInfoSinistro());

            using var controller = new SinistrosController(mockAppSin.Object)
            {
                ControllerContext = mockControllerContext.Object
            };
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var result = controller.VisualizarSinistro(sinistroID);
            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<InfoSinistroViewModel>(viewResult.ViewData.Model);
            Assert.Equal(sinistroID, model.ID);
            Assert.Equal(ufOrigem, model.UFOrigem);
            Assert.Equal(ufDestino, model.UFDestino);
            Assert.Equal(ufSinistro, model.UFSinistro);
            Assert.Equal(motorista, model.NomeMotorista);
        }

        #endregion

        #region Criando os itens que serão usados nos cenários

        public List<SinistrosViewModel> GetSinistrosCliente()
        {
            return new List<SinistrosViewModel>()
            {
                new SinistrosViewModel() { ID = 1, DataSinistro = Convert.ToDateTime("2019-09-27 01:30:00.0000000"), Pendente = true, Seguradora = "SURA - ROYAL & SUNAL", Ramo = "Ramo Teste 1", Protocolo = 52193, UltimoStatus = "SEM INDENIZAÇÃO", Valor = 123.15m },
                new SinistrosViewModel() { ID = 2, DataSinistro = Convert.ToDateTime("2019-08-03 05:24:00.0000000"), Pendente = false, Seguradora = "ARGO SEGUROS BRASIL", Ramo = "Ramo Teste 2", Protocolo = 2, UltimoStatus = "SEM REGULAÇÃO - ANÁLISE CIA", Valor = 155.43m },
                new SinistrosViewModel() { ID = 3, DataSinistro = Convert.ToDateTime("2019-10-07 11:12:00.0000000"), Pendente = true, Seguradora = "AXA - SUL AMERICA CO", Ramo = "Ramo Teste 3", Protocolo = 40884, UltimoStatus = "PAGO", Valor = 250.35m }
            };
        }

        public List<SinistrosProdutorViewModel> GetSinistrosProdutor()
        {
            return new List<SinistrosProdutorViewModel>()
            {
                new SinistrosProdutorViewModel() { ID = 1, Segurado = "JC DA SILVA TRANSPORTES ME", DataSinistro = Convert.ToDateTime("2019-09-27 01:30:00.0000000"), Pendente = true, Seguradora = "SURA - ROYAL & SUNAL", Ramo = "Ramo Teste 1", Protocolo = 52193, UltimoStatus = "SEM INDENIZAÇÃO", Valor = 123.15m },
                new SinistrosProdutorViewModel() { ID = 2, Segurado = "J A COMERCIO DE GENEROS ALIM.E SERV. LTDA", DataSinistro = Convert.ToDateTime("2019-08-03 05:24:00.0000000"), Pendente = false, Seguradora = "ARGO SEGUROS BRASIL", Ramo = "Ramo Teste 2", Protocolo = 12354, UltimoStatus = "SEM REGULAÇÃO - ANÁLISE CIA", Valor = 155.43m },
                new SinistrosProdutorViewModel() { ID = 3, Segurado = "TRANSPORTES WEISS EIRELLI ME", DataSinistro = Convert.ToDateTime("2019-10-07 11:12:00.0000000"), Pendente = true, Seguradora = "AXA - SUL AMERICA CO", Ramo = "Ramo Teste 3", Protocolo = 40884, UltimoStatus = "PAGO", Valor = 250.35m }
            };
        }

        private InfoSinistroViewModel GetInfoSinistro()
        {
            return new InfoSinistroViewModel()
            {
                ID = 2534,
                Cliente = "SHALLON SERVICE LTDA",
                Protocolo = 64100,
                Seguradora = "SURA - ROYAL & SUNAL",
                Reguladora = "Reguradora Teste",
                Ramos = "RCF-DC",
                DataSinistro = Convert.ToDateTime("2015-12-14 00:00:00.0000000"),
                DataAviso = Convert.ToDateTime("2015-12-14 00:00:00.0000000"),
                TiposSinistros = "ROUBO",
                NaturezaSinistro = "ROUBO",
                ValorCarga = 56268.16m,
                ValorIndenizado = 42201.12m,
                ValorSinistro = 56268.16m,
                DataConclusao = Convert.ToDateTime("2016-03-07 00:00:00.0000000"),
                DataIndenizacao = Convert.ToDateTime("2016-03-10 00:00:00.0000000"),
                UltimoStatus = "PAGO",
                CidadeOrigem = "BARUERI",
                CidadeDestino = "SAO PAULO",
                Mecadoria = "DIVERSOS",
                NomeMotorista = "Carlão Tiozão do Churrasco",
                UFOrigem = "SP",
                UFDestino = "RJ",
                UFSinistro = "MG",
                PlacaVeiculo = "EIR4249",
                PlacaCarreta = "EIR4245",
                LocalSinistro = "SP 330",
                CidadeSinistro = "CAJAMAR",
            };
        }

        #endregion

        #region DataDriven

        public static IEnumerable<object[]> ListarSinistrosClienteData()
           => new[]
           {
                new object[] { 3, 2, "ARGO SEGUROS BRASIL", "SEM REGULAÇÃO - ANÁLISE CIA" }
           };

        public static IEnumerable<object[]> ListarSinistrosProdutorData()
           => new[]
           {
                new object[] { 3, 3, "TRANSPORTES WEISS EIRELLI ME", "Ramo Teste 3" }
           };

        public static IEnumerable<object[]> GetInfoSinistroByIdData()
           => new[]
           {
                        new object[] { 2534, "SP", "RJ", "MG", "Carlão Tiozão do Churrasco" }
           };

        #endregion
    }
}
