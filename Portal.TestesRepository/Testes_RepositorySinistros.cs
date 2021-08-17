using Microsoft.EntityFrameworkCore;
using Portal.Core.Entities;
using Portal.Infra.Configuration;
using Portal.Infra.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Portal.Testes_Repository
{
    public class Testes_RepositorySinistros
    {
        /* ============== Montagem de Cenário ============== */
        #region Start

        bool resultado = false;
        private readonly ITestOutputHelper output;
        private readonly DbContextOptions<Context> options;
        private readonly Context context;
        private readonly RepositorySinistros DbSinistros;
        private readonly RepositoryUnidades DbUnidades;
        private readonly RepositorySeguradoras DbSeguradoras;
        private readonly RepositoryClientes DbClientes;
        private readonly RepositoryRamosSeguros DbRamosSeguros;
        private readonly RepositoryRel_Usuarios_Produtores DbRel_Usuarios_Produtores;
        private readonly RepositoryProdutores DbProdutores;
        private readonly RepositoryProdutores_Clientes DbProdutores_Clientes;

        /// <summary>
        /// Construtor Testes_RepositorySinistros
        /// </summary>
        public Testes_RepositorySinistros(ITestOutputHelper output)
        {
            this.output = output;
            var builder = new DbContextOptionsBuilder<Context>();
            builder.UseInMemoryDatabase(databaseName: "dbTest");
            options = builder.Options;
            context = new Context(options);
            DbSinistros = new RepositorySinistros(context);
            DbUnidades = new RepositoryUnidades(context);
            DbSeguradoras = new RepositorySeguradoras(context);
            DbClientes = new RepositoryClientes(context);
            DbRamosSeguros = new RepositoryRamosSeguros(context);
            DbProdutores_Clientes = new RepositoryProdutores_Clientes(context);
            DbProdutores = new RepositoryProdutores(context);
            DbRel_Usuarios_Produtores = new RepositoryRel_Usuarios_Produtores(context);
        }

        #endregion

        #region Testes de Métodos do RepositorySinistros

        /// <summary>
        /// /// <summary>
        /// Teste o método SelectSinistrosClienteByclienteId com base no Id do Cliente.
        /// Ele verifica se o retorno está vindo diferente de vazio, se retorna 2 resultados
        /// se o Id do Sinistro é igual a sinistroId, se o nome da Seguradora é igual a seguradora e
        /// se o Ramo é igual a ramo.
        /// </summary>
        /// <param name="clienteid">Id do Cliente</param>
        /// <param name="count">Quantidade de registros retornados</param>
        /// <param name="sinistroId">Id do Sinistro</param>
        /// <param name="seguradora">Nome da Seguradora</param>
        /// <param name="ramo">Tipo de Ramo</param>
        //[Theory]
        [MemberData(nameof(SinistroClienteData))]
        public void TesteSelectSinistrosClienteByclienteId(long clienteid, int count, long sinistroId, string seguradora, string ramo, string apelido)
        {
            //Limpar Dados
            Limpar();
            output.WriteLine($"Adicinando Itens nas tabelas.");
            //arrange
            ArrangeSelectSinistrosClienteByclienteId();
            //Act
            var retorno = DbSinistros.SelectSinistrosClienteByClienteId(clienteid);
            if (retorno != null)
            {
                output.WriteLine($"O result não está vazio");
                Assert.Equal(count, retorno.Count);
                output.WriteLine($"Encontrou " + count + " resultados");
                foreach (var item in retorno)
                {
                    if (item.ID.Equals(sinistroId))
                    {
                        output.WriteLine($"O Id = " + sinistroId);
                        if (item.Seguradora.Equals(seguradora))
                        {
                            output.WriteLine($"A Seguradora é a " + seguradora);
                            if (item.Ramo.Equals(ramo))
                            {
                                output.WriteLine($"O Ramo é a " + ramo);
                                if (item.Apelido.Equals(apelido))
                                {
                                    output.WriteLine($"O Apelido é o " + apelido);
                                    resultado = true;
                                    output.WriteLine($"Resultado do teste - OK!");
                                    break;
                                }
                            }
                        }
                    }
                }
                Assert.True(resultado);
            }
        }

        /// <summary>
        /// Teste o método SelectByTiposSinistrosID com base no Id do Sinistro.
        /// Ele verifica se o TiposSinistrosID é igual a result.
        /// </summary>
        /// <returns>Retorna o TiposSinistrosID</returns>
        //[Theory]
        //[InlineData(1, 1)]
        //[InlineData(2, 5)]
        [MemberData(nameof(SelectByTiposSinistrosIDData))]
        public void TesteSelectByTiposSinistrosID(long TiposSinistrosID, int result)
        {
            //Limpar Dados
            Limpar();
            output.WriteLine($"Adicinando Item.");
            //arrange
            ArrangeSelectByTiposSinistrosID();
            //Act
            var retorno = DbSinistros.SelectByTiposSinistrosID(TiposSinistrosID);
            //Assert
            output.WriteLine($"Verificando resultado.");
            Assert.True(retorno.Equals(result));
            output.WriteLine($"Resultado do teste - OK!");
        }

        #endregion

        #region Arrange

        /// <summary>
        /// Preenche a tabela de Sinistro que será usadas no TesteSelectByTiposSinistrosID.
        /// </summary>
        private void ArrangeSelectByTiposSinistrosID()
        {
            DbSinistros.Add(new Sinistros() { Id = 1, TiposSinistrosID = 1 });
            DbSinistros.Add(new Sinistros() { Id = 2, TiposSinistrosID = 5 });
            DbSinistros.SaveChanges();
        }

        /// <summary>
        /// Preenche as tabelas de Sinistro, Seguradoras, RamosSeguros, Unidades e Clientes que será usadas no TesteSelectSinistrosClienteByclienteId.
        /// </summary>
        private void ArrangeSelectSinistrosClienteByclienteId()
        {

            DbSeguradoras.Add(new Seguradoras()
            {
                Id = 1,
                NomePrincipal = "ARGO SEGUROS BRASIL",
                Apelido = "TesteApelido1"
            });
            DbSeguradoras.Add(new Seguradoras()
            {
                Id = 2,
                NomePrincipal = "TESTE NOME SEGURADORA LTDA",
                Apelido = "TesteApelido2"
            });
            DbSeguradoras.SaveChanges();

            DbRamosSeguros.Add(new RamosSeguros()
            {
                Id = 1,
                Tipo = "RCTR-C",
            });
            DbRamosSeguros.Add(new RamosSeguros()
            {
                Id = 2,
                Tipo = "RCF-DC",
            });
            DbRamosSeguros.SaveChanges();

            DbUnidades.Add(new Unidades()
            {
                Id = 1,
                ClientesID = 11,
            });
            DbUnidades.Add(new Unidades()
            {
                Id = 2,
                ClientesID = 11,
            });
            DbUnidades.SaveChanges();

            DbClientes.Add(new Clientes()
            {
                Id = 11,
                NomePrincipal = "SHALLON TESTE",
            });
            DbClientes.SaveChanges();

            DbSinistros.Add(new Sinistros()
            {
                Id = 1,
                DataSinistro = Convert.ToDateTime("2012-09-01 00:00:00.0000000"),
                Situacao = "PAGO",
                Protocolo = 1007,
                ValorCarga = Convert.ToDecimal(46055.46),
                UnidadesID = 1,
                SeguradorasID = 2,
                RamosSegurosID = 1
            });
            DbSinistros.Add(new Sinistros()
            {
                Id = 2,
                DataSinistro = Convert.ToDateTime("2012-10-30 00:00:00.0000000"),
                Situacao = "EM REGULAÇÃO - REGULADORA AGUARDANDO DOCTOS",
                Protocolo = 1029,
                ValorCarga = Convert.ToDecimal(52110.38),
                UnidadesID = 2,
                SeguradorasID = 1,
                RamosSegurosID = 2
            });
            DbSinistros.SaveChanges();
        }

        /// <summary>
        /// Limpa as tabelas dos testes que foram preenchidas
        /// </summary>
        private void Limpar()
        {
            var s = context.Sinistros.ToList();
            if (s != null)
            {
                foreach (var item in s)
                {
                    context.Sinistros.Remove(item);
                }
            }

            var c = context.Clientes.ToList();
            if (c != null)
            {
                foreach (var item in c)
                {
                    context.Clientes.Remove(item);
                }
            }

            var u = context.Unidades.ToList();
            if (u != null)
            {
                foreach (var item in u)
                {
                    context.Unidades.Remove(item);
                }
            }

            var sg = context.Seguradoras.ToList();
            if (sg != null)
            {
                foreach (var item in sg)
                {
                    context.Seguradoras.Remove(item);
                }
            }

            var r = context.RamosSeguros.ToList();
            if (r != null)
            {
                foreach (var item in r)
                {
                    context.RamosSeguros.Remove(item);
                }
            }
        }

        #endregion

        #region DataDriven

        public static IEnumerable<object[]> SinistroClienteData()
            => new[]
            {
                new object[] { 11, 2, 1, "TESTE NOME SEGURADORA LTDA", "RCTR-C", "TesteApelido2" },
                new object[] { 11, 2, 2, "ARGO SEGUROS BRASIL", "RCF-DC", "TesteApelido1" },
            };

        public static IEnumerable<object[]> SelectByTiposSinistrosIDData()
            => new[]
            {
                new object[] { 1, 1 },
                new object[] { 2, 5 },
            };

        #endregion
    }
}
