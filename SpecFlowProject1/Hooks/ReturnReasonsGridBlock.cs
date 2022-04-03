using DigitalPaymentsCommon.Models.Elements;
using OpenQA.Selenium;
using ProcessOneCommon.PageAbstractions;

namespace ProcessOneCommon.UnicornPages.Administration
{
    public class ReturnReasonsGridBlock : BasePage
    {
        private readonly IWebElement _webElement;
        
        private readonly By _returnReasonsTypeLocator = By.XPath(".//td[@data-field='Type']");
        
        public ReturnReasonsGridBlock(IWebDriver webDriver, IWebElement element) : base(webDriver)
        {
            _webElement = element;
        }

        public TextField ReturnReasonsTypeField => new TextField(WebDriver, _webElement, _returnReasonsTypeLocator);
    }
}