using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using System;

namespace Selenium.Utils
{
    public static class WebDriverExtensions
    {
        /// <summary>
        /// Verifica o TimeOut da Pagina, se demorar o teste deu errado.
        /// </summary>
        /// <param name="webDriver">Instancia de IWebDriver</param>
        /// <param name="timeToWait">Tempo de Espera</param>
        /// <param name="url">url que será testada</param>
        public static void LoadPage(this IWebDriver webDriver, TimeSpan timeToWait, string url)
        {
            webDriver.Manage().Timeouts().PageLoad = timeToWait;
            webDriver.Navigate().GoToUrl(url);
        }

        /// <summary>
        /// Para manupular Elementos do JavaScript
        /// </summary>
        /// <param name="webDriver">Instancia de IWebDriver</param>
        /// <param name="by">Instancia de By para manipular ID, Names, Classes etc...</param>
        /// <returns></returns>
        public static string GetText(this IWebDriver webDriver, By by)
        {
            var webElement = webDriver.FindElement(by);
            return webElement.Text;
        }

        /// <summary>
        /// Para manupular Elementos do JavaScript e limpa o campo e simulando um texto que será digitado
        /// </summary>
        /// <param name="webDriver">Instancia de IWebDriver</param>
        /// <param name="by">Instancia de By para manipular ID, Names, Classes etc...</param>
        /// <returns></returns>
        public static void SetText(this IWebDriver webDriver, By by, string text)
        {
            var webElement = webDriver.FindElement(by);
            webElement.Clear();
            webElement.SendKeys(text);
        }

        /// <summary>
        /// Para fazer um Submit
        /// </summary>
        /// <param name="webDriver">Instancia de IWebDriver</param>
        /// <param name="by">Instancia de By para manipular ID, Names, Classes etc...</param>
        /// <returns></returns>
        public static void Submit(this IWebDriver webDriver, By by)
        {
            var webElement = webDriver.FindElement(by);
            if (!(webDriver is InternetExplorerDriver))
            {
                webElement.Submit();
            }
            else
            {
                webElement.SendKeys(Keys.Enter);
            }
        }
    }
}
