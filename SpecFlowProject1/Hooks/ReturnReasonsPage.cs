using System;
using System.Collections.Generic;
using System.Linq;
using DigitalPaymentsCommon;
using DigitalPaymentsCommon.Dictionaries;
using DigitalPaymentsCommon.Helpers;
using DigitalPaymentsCommon.Models.Elements;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using ProcessOneCommon.PageAbstractions;
using Utils;
using Utils.Extensions;

namespace ProcessOneCommon.UnicornPages.Administration
{
    public class ReturnReasonsPage : BasePage
    {
        private static readonly By _createNewReasonButtonLocator = By.Id("AddReturnReasonButton");
        private readonly By _gridRowLocator = new ByChained(By.Id("ReturnReasonsGrid"), By.XPath(".//tbody//tr"));
        private readonly By _notificationLocator = By.CssSelector("div.notify-container");
        private readonly By _editButtonLocator = By.XPath(".//td//button[@class='edit-button']");
        private static readonly By _searchReasonFieldLocator = By.Id("SearchPattern");
        private readonly By _tableColumnHeaderLocator = By.XPath("//div[contains(@id, 'ReturnReasonsTab')]//th[not (@style = 'display:none') and contains(@data-role, 'columnsorter')]");
        private readonly By _returnReasonRowLocator = By.XPath("//tr[@role = 'row' and @data-uid]");

        public ReturnReasonsPage(IWebDriver webDriver) : base(webDriver) { }

        public Button CreateNewReasonButton => new Button(WebDriver, _createNewReasonButtonLocator);

        public Button EditButton => new Button(WebDriver, _editButtonLocator);

        public Input SearchReasonFieldInput => new Input(WebDriver, _searchReasonFieldLocator);
        
        public Notification Notification => new Notification(WebDriver, _notificationLocator);

        public List<ReturnReasonsGridBlock> ReturnReasonsBlocks =>
            ControlHelper.GetCollectionWithElementsOrEmpty(WebDriver, _returnReasonRowLocator).Select(e => new ReturnReasonsGridBlock(WebDriver, e))
                .ToList();

        private ColumnSortType GetColumnSortType(int columnIndex)
        {
            var sortingColumn = TableColumnHeaders.ElementAt(columnIndex);
            var sortingOrder = sortingColumn.GetAttribute("data-dir");

            switch (sortingOrder)
            {
                case "asc":
                    return ColumnSortType.Asc;

                case "desc":
                    return ColumnSortType.Desc;

                default:
                    return ColumnSortType.None;
            }
        }
        
        public List<IWebElement> TableColumnHeaders =>
            ControlHelper.GetCollectionWithElements(WebDriver, _tableColumnHeaderLocator).ToList();
        
        public List<object> GetSortedAttributesList(string columnName)
        {
            var attributeName = EnumExtensions.GetValueFromDescription<ReturnReasonsTableColumnNameEnum>(columnName).ToString();

            var transactionsAttributeValueList = ReturnReasonsBlocks
                .Select(t => t.GetType().GetProperty(attributeName)?.GetValue(t).ToString()).ToList();

            columnName = columnName.ToLower();

            if (columnName.Contains("amount"))
            {
                var listOfSortedAttributes = transactionsAttributeValueList.Select(t =>
                    t == string.Empty ? "0" : t.Replace("(", "-").Trim(')').Replace("$", string.Empty)).ToList();

                return listOfSortedAttributes.Select(t => (object)decimal.Parse(t)).ToList();
            }

            if (columnName.Contains("id") || columnName.Contains("#") && !columnName.Contains("customer") && !columnName.Contains("external") &&
                !columnName.Contains("policy"))
            {
                return transactionsAttributeValueList.Select(t => (object)int.Parse(t == string.Empty ? "0" : t)).ToList();
            }

            return transactionsAttributeValueList.Select(t => (object)t).ToList();
        }

        public void SortByColumnNameAsc(ReturnReasonsTableColumnNameEnum columnName)
        {
            var sortingColumn = TableColumnHeaders.ElementAt((int)columnName);
            sortingColumn.GetAttribute("innerText").Should().Be(columnName.GetDescription());
            var sortType = GetColumnSortType((int)columnName);

            if (sortType == ColumnSortType.None)
            {
                ControlHelper.Click(sortingColumn);
            }

            GetColumnSortType((int) columnName).Should().Be(ColumnSortType.Asc);
        }

        public void SortByColumnNameDesc(ReturnReasonsTableColumnNameEnum columnName)
        {
            var sortingColumn = TableColumnHeaders.ElementAt((int)columnName);
            sortingColumn.GetAttribute("innerText").Should().Be(columnName.GetDescription());
            var sortType = GetColumnSortType((int)columnName);
            ControlHelper.Click(sortingColumn);

            if (sortType == ColumnSortType.None)
            {
                ControlHelper.Click(sortingColumn);
            }
            
            GetColumnSortType((int) columnName).Should().Be(ColumnSortType.Desc);
        }

        public List<ReturnReasonsGridBlock> ReturnReasonsGridBlocks =>
            ControlHelper.GetCollectionWithElements(WebDriver, _gridRowLocator)
                .Select(e => new ReturnReasonsGridBlock(WebDriver, e)).ToList();
        
        public Pager ReturnReasonsGridBlocksPager => new Pager(WebDriver);

        public ReturnReasonsGridBlock GetReturnReasonsGridBlockByName(string name)
        {
            var pagesCount = ReturnReasonsGridBlocksPager.GetPagesCount();

            if (pagesCount == 1)
            {
                return ReturnReasonsGridBlocks.Find(x => x.ReturnReasonsTypeField.Text.Equals(name));
            }

            ReturnReasonsGridBlocksPager.GoToFirstPage();
            
            for (var pageNumber = 1; pageNumber <= pagesCount; pageNumber++)
            {
                var block = ReturnReasonsGridBlocks.Find(x => x.ReturnReasonsTypeField.Text.Equals(name));
                
                if (block != null)
                {
                    return block;
                }

                if (pageNumber != pagesCount)
                {
                    ReturnReasonsGridBlocksPager.GoToNextPage();
                }
            }
            
            Assert.Fail($"No Return Reasons found with name: {name}");
            return null;
        }
    }
}