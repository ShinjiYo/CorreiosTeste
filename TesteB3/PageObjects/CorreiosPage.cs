using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using CorreiosTeste.Hooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorreiosTeste.PageObjects
{
    internal class CorreiosPage { 


        IWebDriver driver;// iniciando o driver para ajudar na coleta dos elementos

        /// <summary>
        /// Inicialização dos elementos
        /// </summary>
        
        //Elemento do input do CEP
        IWebElement inputBuscaCep => driver.FindElement(By.Id("relaxation")); // por ID
        // IWebElement inputBuscaCep => driver.FindElement(By.XPath("//*[(@id = \"relaxation\")]")); // por XPath
        // IWebElement inputBuscaCep => driver.FindElement(By.CssSelector("#relaxation")); // por CSS

        //Elemento do botão do buscar CEP
        IWebElement botaoBuscaCep => driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[3]/div/div/article/div[3]/div/div[2]/div[2]/form/div[2]/button/i"));
        // Neste caso apenas com o XPath foi possível

        //Elemento de mensagem de erro do buscar CEP
        IWebElement mensagemErroCEP => driver.FindElement(By.Id("mensagem-resultado-alerta")); // por ID
        //IWebElement mensagemErroCEP => driver.FindElement(By.XPath("/html/body/main/form/div[1]/div[2]/div/div[3]/h6")); // por XPath
        //IWebElement mensagemErroCEP => driver.FindElement(By.CssSelector("#mensagem-resultado-alerta")); // por CSS

        //Elemento de logradouro do CEP
        //IWebElement textoLogradouro => driver.FindElement(By.XPath("/html/body/main/form/div[1]/div[2]/div/div[4]/table/tbody/tr/td[1]")); // por XPath
        IWebElement textoLogradouro => driver.FindElement(By.CssSelector("td:nth-child(1)")); // por CSS

        //Elemento de localidade e uf do CEP
        //IWebElement textoUF => driver.FindElement(By.XPath("/html/body/main/form/div[1]/div[2]/div/div[4]/table/tbody/tr/td[2]")); // por XPath
        IWebElement textoUF => driver.FindElement(By.CssSelector("td:nth-child(3)")); // por CSS

        //Elemento do input da encomenda
        IWebElement inputBuscaEncomenda => driver.FindElement(By.Id("objetos")); // por ID
        // IWebElement inputBuscaEncomenda => driver.FindElement(By.XPath("//*[@id="objetos"]")); // por XPath
        // IWebElement inputBuscaEncomenda => driver.FindElement(By.CssSelector("#objetos")); // por CSS

        //Elemento do botão de buscar encomenda
        IWebElement botaoBuscaEncomenda => driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[3]/div/div/article/div[3]/div/div[2]/div[1]/form/div[2]/button/i"));
        //Elemento do botão de buscar encomenda na segunda página
        IWebElement botaoBuscaEncomendaSegPag => driver.FindElement(By.XPath("/html/body/main/div[1]/form/div[2]/div[1]/div/div[2]/button/i"));
        //Elemento alerta da mensagem da encomenda
        IWebElement alertaEncomenda => driver.FindElement(By.CssSelector(".msg"));

        //Elemento de input do CAPTCHA  
        IWebElement inputCaptcha => driver.FindElement(By.Id("captcha"));

        //Atualmente existe uma assistente virtual no site que atrapalha alguns testes entao, utilizerei esse findByElement para acha-la e poder fechar
        IWebElement carolFecha => driver.FindElement(By.Id("carol-fecha"));

        
       
        
        public CorreiosPage()
        {
            driver = Hook.driver;
        }

        public void AcessarSiteCorreios()
        {
            driver.Navigate().GoToUrl("https://www.correios.com.br");
        }

        public void InserirCEP(String CEP)
        {
            inputBuscaCep.SendKeys(CEP);
        }

        public void ApagarCEP()
        {
            inputBuscaCep.Clear();
        }
        public void BuscarCEP()
        {
            botaoBuscaCep.Click();
        }
        public void fecharCarol()
        {
            carolFecha.Click();
        }

        public String MensagemErroCEP()
        {
            return mensagemErroCEP.Text;
        }

        public void TrocarAba() //Sempre vai trocar para a segunda aba
        {
            driver.SwitchTo().Window(driver.WindowHandles[1]);
        }

        public void FecharAba() // Sempre vai fechar a segunda aba e retornar a principal
        {
            driver.SwitchTo().Window(driver.WindowHandles[1]).Close();
            driver.SwitchTo().Window(driver.WindowHandles.First());
        }

        public String TextoLogradouro()
        {
            return textoLogradouro.Text;
        }

        public String TextoUF()
        {
            return textoUF.Text;
        }

        public void InserirCodEncomenda(String codEncomenda)
        {
            inputBuscaEncomenda.SendKeys(codEncomenda);
        }
        public void BuscarEncomenda()
        {
            botaoBuscaEncomenda.Click();
        }
        public void BuscarEncomendaSegundaPagina()
        {
            Thread.Sleep(10000);
            botaoBuscaEncomendaSegPag.Click();
        }

        public void selecionarCaptcha()
        {
            inputCaptcha.Click();
        }
        public String mensagemBuscarEncomenda()
        {
            Thread.Sleep(1000);
            return alertaEncomenda.Text;
        }
    }
}
