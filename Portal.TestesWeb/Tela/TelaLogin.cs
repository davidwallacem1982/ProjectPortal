using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Selenium.Utils;
using System;

namespace Portal.TestesWeb
{
    public class TelaLogin
    {
        private readonly IWebDriver _driver;

        public TelaLogin(Browser browser)
        {
            //_browser = browser;
            _driver = WebDriverFactory.CreateWebDriver(browser);
        }

        public void CarregarPagina()
        {
            _driver.Manage().Window.Maximize();
            _driver.LoadPage(TimeSpan.FromSeconds(15), "https://portaltradevale.azurewebsites.net/");
        }

        public void PreencherLogin(string login, string senha)
        {
            _driver.SetText(By.Name("UserName"), login);
            _driver.SetText(By.Name("Password"), senha);
        }

        public void AcessarPortal()
        {
            _driver.Submit(By.Id("testbtn"));
        }

        public bool VerificarTitulo(string titulo)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            return wait.Until(d => d.Title == titulo);
        }

        public bool VerificarErro(string errmsg)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            return wait.Until(d => d.FindElement(By.Id("errmsg")).Text == errmsg);
        }

        public void Fechar()
        {
            _driver.Quit();
        }

        //Método para capturar screenshot da tela
        public void Screenshot()
        {
            ITakesScreenshot camera = _driver as ITakesScreenshot;
            Screenshot foto = camera.GetScreenshot();
            foto.SaveAsFile(@"C:\Temp\TestePortal\", ScreenshotImageFormat.Png);
        }
    }
}
