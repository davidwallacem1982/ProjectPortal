using Microsoft.EntityFrameworkCore;
using Portal.Core.Entities;
using Portal.Core.Interfaces;
using Portal.Infra.Configuration;
using Portal.Infra.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Portal.Testes_Repository
{
    public class Testes_RepositoryContatosPortal
    {
        /* ============== Montagem de Cenário ============== */
        #region Start

        bool resultado = false;
        private readonly ITestOutputHelper output;
        private readonly DbContextOptions<Context> options;
        private readonly Context context;
        private readonly IRepositoryContatosPortal DbContatosPortal;

        /// <summary>
        /// Construtor Testes_RepositorySinistros
        /// </summary>
        public Testes_RepositoryContatosPortal(ITestOutputHelper output)
        {
            this.output = output;
            var builder = new DbContextOptionsBuilder<Context>();
            builder.UseInMemoryDatabase(databaseName: "dbTest");
            options = builder.Options;
            context = new Context(options);
            DbContatosPortal = new RepositoryContatosPortal(context);
        }

        #endregion

        #region Testes de Métodos do RepositoryContatosPortal

        /// <summary>
        /// /// <summary>
        /// Teste o método SelectContatos.
        /// Ele verifica se o retorno está retornando uma lista de tiulos de contatos.
        /// </summary>
        /// <param name="count">Quantidade de registros retornados</param>
        /// <param name="Titulo">Titulo do Contato</param>
        //[Theory]
        [MemberData(nameof(ContatosPortalData))]
        public void SelectContatos(int count, string titulo)
        {
            //Limpar Dados
            Limpar();

            output.WriteLine($"Adicinando os itens nas tabelas.");
            //arrange
            ArrangeSelectContatos();

            //Act
            var retorno = DbContatosPortal.SelectContatos();

            if (retorno != null)
            {
                output.WriteLine($"O retorno do teste não está vazio");

                Assert.Equal(count, retorno.Count);

                output.WriteLine($"O teste encontrou " + count + " resultados");

                foreach (var item in retorno)
                {
                    if (item.Titulo.Equals(titulo))
                    {
                        output.WriteLine($"O teste encontrou a variável Titulo = " + titulo);

                        resultado = true;

                        output.WriteLine($"Resultado do teste - OK!");
                        break;
                    }
                }
                Assert.True(resultado);
            }
        }

        #endregion

        #region Limpar Tabelas
        /// <summary>
        /// Limpa as tabelas dos testes que foram preenchidas
        /// </summary>
        private void Limpar()
        {
            var s = context.ContatosPortal.ToList();
            if (s != null)
            {
                foreach (var item in s)
                {
                    context.ContatosPortal.Remove(item);
                }
            }
        }
        #endregion

        #region Arrange

        /// <summary>
        /// Preenche a tabela de Sinistro que será usadas no Testes_RepositoryContatosPortal.
        /// </summary>
        private void ArrangeSelectContatos()
        {
            DbContatosPortal.Add(new ContatosPortal() { Id = 1, Titulo = "Técnica de Transportes Nacional (Embarcador e Transportador) e Pedido de Cobertura Extra" });
            DbContatosPortal.Add(new ContatosPortal() { Id = 2, Titulo = "Técnica de Transportes Internacional" });
            DbContatosPortal.Add(new ContatosPortal() { Id = 3, Titulo = "Titulo 4" });
            DbContatosPortal.Add(new ContatosPortal() { Id = 4, Titulo = "Pendência de Pagamentos" });
            DbContatosPortal.SaveChanges();
        }

        #endregion

        #region DataDriven

        /// <summary>
        /// Preenche os valores que você quer que retorne.
        /// </summary>
        public static IEnumerable<object[]> ContatosPortalData()
            => new[]
            {
                new object[] { 4, "Pendência de Pagamentos" },
            };

        #endregion
    }
}
