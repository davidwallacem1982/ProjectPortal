using System.Text;

namespace Portal.Web.UtilWeb
{
    public class MakeEmail
    {
        /// <summary>
        /// Recuperação do Layout do Email de Confirmação de Usuário para o cliente
        /// </summary>
        /// <param name="Nome">Nome do Usuário</param>
        /// <param name="urlConfirmacao">URL para Confirmação</param>
        public string MakeEmailNewUser(string Nome, string urlConfirmacao)
        {
            var serverPath = System.IO.Path.GetFullPath("wwwroot\\configuration\\emailNewUser.txt");
            var fileContents = System.IO.File.ReadAllText(serverPath, Encoding.UTF8);

            fileContents = fileContents.Replace("[Nome]", Nome);
            fileContents = fileContents.Replace("[urlConfirmacao]", urlConfirmacao);

            return fileContents;
        }

        /// <summary>
        /// Recuperação do Layout do Email de Recuperação de Senha para o cliente
        /// </summary>
        /// <param name="Nome">Nome do Usuário</param>
        /// <param name="urlConfirmacao">URL para Confirmação</param>
        public string MakeEmailRecuperaSenha(string Nome, string urlConfirmacao)
        {
            var serverPath = System.IO.Path.GetFullPath("wwwroot\\configuration\\emailRecuperaSenha.txt");
            var fileContents = System.IO.File.ReadAllText(serverPath, Encoding.UTF8);

            fileContents = fileContents.Replace("[Nome]", Nome);
            fileContents = fileContents.Replace("[urlConfirmacao]", urlConfirmacao);

            return fileContents;
        }
    }
}
