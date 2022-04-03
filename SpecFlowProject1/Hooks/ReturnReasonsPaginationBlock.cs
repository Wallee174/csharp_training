using DigitalPaymentsCommon.Models.Elements;
using OpenQA.Selenium;
using ProcessOneCommon.PageAbstractions;

namespace ProcessOneCommon.UnicornPages.Administration
{
    public class ReturnReasonsPaginationBlock : BasePage
    {
        private readonly By _paginationClickGoToFirstPageLocator = By.CssSelector("#ReturnReasonsGrid a.k-link.k-pager-nav.k-pager-first");
        private readonly By _paginationClickGoToPreviousPageLocator = By.CssSelector("#ReturnReasonsGrid a.k-link.k-pager-nav span.k-i-arrow-60-left");
        private readonly By _paginationGoToNextPageLocator = By.CssSelector("#ReturnReasonsGrid a.k-link.k-pager-nav span.k-i-arrow-60-right");
        private readonly By _paginationGoToLastPageLocator = By.CssSelector("#ReturnReasonsGrid a.k-link.k-pager-nav.k-pager-last");
        private readonly By _paginationInfoLocator = By.CssSelector("#ReturnReasonsGrid span.k-pager-info");
        
        public ReturnReasonsPaginationBlock(IWebDriver webDriver) : base(webDriver)
        {
        }

        public Button PaginationFirstPage => new Button(WebDriver, _paginationClickGoToFirstPageLocator);

        public Button PaginationPreviousPage => new Button(WebDriver, _paginationClickGoToPreviousPageLocator);

        public Button PaginationNextPage => new Button(WebDriver, _paginationGoToNextPageLocator);

        public Button PaginationLastPage => new Button(WebDriver, _paginationGoToLastPageLocator);

        public TextField PaginationInfoText => new TextField(WebDriver, _paginationInfoLocator);
    }
}