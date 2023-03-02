using System;
using System.Reflection;
using TechTalk.SpecFlow;
using CorreiosTeste.PageObjects;
using NUnit.Framework;
using System.Runtime.ConstrainedExecution;

namespace CorreiosTeste.StepDefinition
{
    [Binding]
    public class CorreiosStepDefinitions
    {
        CorreiosPage correiosPage;
        public CorreiosStepDefinitions()
        { 
            correiosPage = new CorreiosPage();
        }

        [Given(@"acessei o site dos correios")]
        public void GivenAcesseiOSiteDosCorreios()
        {
            correiosPage.AcessarSiteCorreios();
            correiosPage.fecharCarol();
        }


        [When(@"busquei o CEP ""([^""]*)""")]
        public void WhenBusqueiOCEP(string CEP)
        {
            correiosPage.InserirCEP(CEP);
            correiosPage.BuscarCEP();
        }

        [Then(@"o sistema retornou a mensagem ""([^""]*)""")]
        public void ThenOSistemaRetornouAMensagem(string msgEsperada)
        {
            correiosPage.TrocarAba();
            var msgRecebida = correiosPage.MensagemErroCEP();
            Assert.That(msgRecebida, Is.EqualTo(msgEsperada), () => "Erro, mensagem diferente!");

        }


        [Then(@"o sistema encontrou o CEP e retornou: ""([^""]*)""")]
        public void ThenOSistemaEncontrouOCEPERetornou(string enderecoEsperado)
        {
            correiosPage.TrocarAba();

            var logradouroRecebido = correiosPage.TextoLogradouro();
            var ufRecebido = correiosPage.TextoUF();
            
            var enderecoRecebido = logradouroRecebido + " " + ufRecebido;

            Assert.That(enderecoRecebido, Is.EqualTo(enderecoEsperado), () => "Erro, endereço diferente!");
        }



        [When(@"busquei a encomenda ""([^""]*)""")]
        public void WhenBusqueiAEncomenda(string codEncomenda)
        {
            
            correiosPage.InserirCodEncomenda(codEncomenda);
            correiosPage.BuscarEncomenda();
            correiosPage.TrocarAba();
            correiosPage.selecionarCaptcha();
            correiosPage.BuscarEncomendaSegundaPagina();

        }

        [Then(@"o sistema retornou o seguinte status sobre a encomenda ""([^""]*)""")]
        public void ThenOSistemaRetornouOSeguinteStatusSobreAEncomenda(string msgEsperada)
        {
            var msgRecebida = correiosPage.mensagemBuscarEncomenda();
            Assert.That(msgRecebida, Is.EqualTo(msgEsperada), () => "Erro, mensagem diferente!");
        }

        [When(@"confirmei que o CEP não existe, com a mensagem ""([^""]*)"", voltei a tela inicial")]
        public void WhenConfirmeiQueOCEPNaoExisteComAMensagemVolteiATelaInicial(string msgEsperada)
        {
            correiosPage.TrocarAba();
            var msgRecebida = correiosPage.MensagemErroCEP();
            Assert.That(msgRecebida, Is.EqualTo(msgEsperada), () => "Erro, mensagem diferente!");
            correiosPage.FecharAba();
        }

        [When(@"apaguei o cep anterior e busquei o CEP ""([^""]*)""")]
        public void WhenApagueiOCepAnteriorEBusqueiOCEP(string CEP)
        {
            correiosPage.ApagarCEP();
            correiosPage.InserirCEP(CEP);
            correiosPage.BuscarCEP();
        }

        [When(@"recebi o resultado: ""([^""]*)"", voltei a tela incial")]
        public void WhenRecebiOResultadoVolteiATelaIncial(string enderecoEsperado)
        {
            correiosPage.TrocarAba();
            var logradouroRecebido = correiosPage.TextoLogradouro();
            var ufRecebido = correiosPage.TextoUF();

            var enderecoRecebido = logradouroRecebido + " " + ufRecebido;
            Console.WriteLine(enderecoRecebido);

            Assert.That(enderecoRecebido, Is.EqualTo(enderecoEsperado), () => "Erro, endereço diferente!");

            correiosPage.FecharAba();
        }

    }
}
