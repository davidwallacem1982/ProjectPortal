using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace Selenium.Utils
{
    public static class WebDriverFactory
    {
        /// <summary>
        /// Representa os Browser que podem ser testados, e foi funcionar me Modo Headless a string headless = "--headless"
        /// </summary>
        /// <param name="browser">Browser</param>
        /// <param name="headless">variavel que representa se o Browser ira funcionar em Modo headless</param>
        /// <returns></returns>
        public static IWebDriver CreateWebDriver(Browser browser)
        {
            IWebDriver webDriver = null;

            switch (browser)
            {
                case Browser.FirefoxHeadless:
                    FirefoxOptions optionsFireFox = new FirefoxOptions();
                    optionsFireFox.AddArgument("--headless");
                    webDriver = new FirefoxDriver(optionsFireFox);
                    break;
                case Browser.ChromeHeadless:
                    ChromeOptions optionsChrome = new ChromeOptions();
                    optionsChrome.AddArgument("--headless");
                    webDriver = new ChromeDriver(optionsChrome);
                    break;
                case Browser.Firefox:
                    webDriver = new FirefoxDriver();
                    break;
                case Browser.Chrome:
                    webDriver = new ChromeDriver();
                    break;
                case Browser.InternetExplorer:
                    webDriver = new InternetExplorerDriver();
                    break;
                case Browser.Edge:
                    webDriver = new EdgeDriver();
                    break;
            }
            return webDriver;
        }
    }
}
