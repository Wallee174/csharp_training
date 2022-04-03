using System.Linq;
using System.Collections.Generic;
using DigitalPaymentsCommon.Helpers;
using DigitalPaymentsCommon.Models.Elements;
using OpenQA.Selenium;
using ProcessOneCommon.Models;
using DigitalPaymentsCommon.Models.Elements;
using DigitalPaymentsCommon.Models.Elements.Dropdown;
using OpenQA.Selenium.Support.PageObjects;


namespace ProcessOneCommon.UnicornPages.Administration
{
    public class ReturnReasonsCreateModal : Modal
    {
        private static readonly By _returnReasonsTypeLocator = By.XPath("//div[@class='col-md-8']//span[contains(@aria-owns, 'Type_listbox')]");
        private static readonly By _codeNameLocator = By.Id("Code");
        private static readonly By _reasonNameLocator = By.Id("Description");
        private static readonly By _saveButtonLocator = By.Id("SaveButton");
        private static readonly By _categoryNameLocator = By.XPath("//span[contains(@aria-owns, 'CardReturnReasonCategoryId_listbox')]");
        private static readonly By _eftReturnActionTypeLocator =
            By.XPath("//span[contains(@aria-owns, 'EftBlockRule_listbox')]");
        private static readonly By _reasonTypeSelectDisabledLocator = By.XPath("//div[@class='col-md-8']//span[contains(@class, 'k-widget k-dropdown k-state-disabled')]");

        private static readonly By _categoryLocator =
            By.XPath("//span[contains(@aria-owns, 'CardReturnReasonCategoryId_label']");

        public Button SaveReasonButton => new Button(WebDriver, _saveButtonLocator);

        public Input CodeNameInput => new Input(WebDriver, _codeNameLocator);

        public KendoDropdownList ReasonTypeSelectDisabled =>
            new KendoDropdownList(WebDriver, _reasonTypeSelectDisabledLocator);

        public Input ReasonsInput => new Input(WebDriver, _reasonNameLocator);

        public KendoDropdownList ReasonTypeSelect => new KendoDropdownList(WebDriver, _returnReasonsTypeLocator);

        public KendoDropdownList ReturnCategorySelect => new KendoDropdownList(WebDriver, _categoryNameLocator);
        
        public KendoDropdownList EFTReturnActionSelect => new KendoDropdownList(WebDriver, _eftReturnActionTypeLocator);

        public ReturnReasonsCreateModal(IWebDriver webDriver) : base(webDriver, modalLocator:By.Id("EditReturnReasonForm"))
        {
        }
    }
}