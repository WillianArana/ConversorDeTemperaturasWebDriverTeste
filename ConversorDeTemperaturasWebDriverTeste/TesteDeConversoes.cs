using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConversorDeTemperaturasWebDriverTeste
{
    [TestFixture]
    public class TesteDeConversoes
    {
        public static IWebDriver driver;

        [SetUp]
        public static void Start()
        {
            const string url = "http://localhost:52694/";
            var browser = Browsers.Firefox;
         
            switch (browser)
            {
                case Browsers.Chrome:
                    driver = new ChromeDriver();
                    break;

                case Browsers.Firefox:
                    driver = new FirefoxDriver();
                    break;

                default:
                    throw new Exception("Um drive deve ser escolhido!");
            }

            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
        }

        [TearDown]
        public static void Stop()
        {
            driver.Quit();
        }

        [Test]
        public static void TestandoConversorDeTemperatura()
        {
            var inputTemp = driver.FindElement(By.Id("CelsiusIdInput"));
            inputTemp.Clear();
            inputTemp.SendKeys("5");
            inputTemp.Submit();

            try
            {
                Thread.Sleep(100);
            }
            catch (ThreadInterruptedException e)
            {
                throw new Exception(e.Message);
            }
           
            var celsius = driver.FindElement(By.Id("CelsiusIdSpan")).Text.Trim();
            var fahrenheit = driver.FindElement(By.Id("FahrenheitIdSpan")).Text.Trim();

            var mensagem = "Não obter resposta";
            Assert.That(celsius, Is.Not.Null.Or.Empty, mensagem);
            Assert.That(fahrenheit, Is.Not.Null.Or.Empty, mensagem);

            mensagem = "Não foi possivel converter!";

            Assert.That(celsius, Is.Not.EqualTo(fahrenheit), mensagem);
        }

    }
}
