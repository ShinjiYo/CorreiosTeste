using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace CorreiosTeste.Hooks
{
    [Binding]
    public class Hook
    {
        public static IWebDriver driver;


        [BeforeScenario(Order = 1)]
        public void FirstBeforeScenario()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [AfterScenario]
        public void AfterScenario()
        {
         driver.Quit();
         driver.Dispose();
        }
    }
}