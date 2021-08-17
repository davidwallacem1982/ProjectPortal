using Selenium.Utils;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace Portal.TestesWeb
{
    public class TestarLogin
    {
        #region Métodos Testes da View de Login

        //[Theory]
        [MemberData(nameof(TesteLoginSeguradoData))]
        public void TesteLoginSegurado(Browser browser, string login, string senha, string titulo)
        {
            var tela = new TelaLogin(browser);

            tela.CarregarPagina();
            tela.PreencherLogin(login, senha);
            tela.AcessarPortal();
            Thread.Sleep(1000);
            tela.Screenshot();
            Thread.Sleep(1000);
            var retorno = tela.VerificarTitulo(titulo);

            Assert.True(retorno);
            tela.Fechar();

        }

        //[Theory]
        [MemberData(nameof(TesteErroLoginData))]
        public void TesteErroLogin(Browser browser, string login, string senha, string erro)
        {
            var tela = new TelaLogin(browser);

            tela.CarregarPagina();
            tela.PreencherLogin(login, senha);
            tela.AcessarPortal();
            var retorno = tela.VerificarErro(erro);

            Assert.True(retorno);
            tela.Fechar();
        }

        #endregion

        #region DataDriven

        public static IEnumerable<object[]> TesteLoginSeguradoData()
           => new[]
           {
                new object[] { Browser.ChromeHeadless, "dev", "135!Mudar", "Sinistros do Segurado - Portal" },
                new object[] { Browser.ChromeHeadless, "davidw", "135!Mudar", "Sinistros do Comercial - Portal" }
           };

        public static IEnumerable<object[]> TesteErroLoginData()
           => new[]
           {
                new object[] { Browser.ChromeHeadless, "dev", "senhaerro", "Senha Inválida!" },
                new object[] { Browser.ChromeHeadless, "davidw", "senhaerro", "Senha Inválida!" },
                new object[] { Browser.ChromeHeadless, "loginerro", "135!Mudar", "Usuário informado não existe no sistema!" },
                new object[] { Browser.ChromeHeadless, "", "", "Preencha todas as informações!" }
           };

        #endregion
    }
}
