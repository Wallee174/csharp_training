using System;
using System.Linq;
using System.Threading;
using FluentAssertions;
using OpenQA.Selenium;
using ProcessOneCommon.Dictionaries;
using ProcessOneCommon.UnicornPages;
using ProcessOneCommon.UnicornPages.Administration;
using TechTalk.SpecFlow;
using Utils;
using Utils.Extensions;
using ConfirmationPage = ProcessOneCommon.LegacyPages.ConfirmationPage;

namespace ProcessOneAdminUI.Bindings.Unicorn
{
    [Binding]
    [Scope(Tag = "ReturnReasons")]
    public class ReturnReasonsSteps : Steps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IWebDriver _webDriver;

        public ReturnReasonsSteps(ScenarioContext scenarioContext, IWebDriver webDriver)
        {
            _scenarioContext = scenarioContext ?? throw new ArgumentNullException(nameof(scenarioContext));
            _webDriver = webDriver ?? throw new ArgumentNullException(nameof(webDriver));
        }

        private ReturnReasonsPage ReturnReasonsPage => new ReturnReasonsPage(_webDriver);

        private ReturnReasonsCreateModal ReturnReasonsCreateModal => new ReturnReasonsCreateModal(_webDriver);

        public ReturnReasonsPaginationBlock ReturnReasonsPaginationSettings =>
            new ReturnReasonsPaginationBlock(_webDriver);

        public int SortingColumnIndex
        {
            get => _scenarioContext.Get<int>("sortingColumnIndex");
            set => _scenarioContext.Set(value, "sortingColumnIndex");
        }
        
        public string SortingColumnName
        {
            get => _scenarioContext.Get<string>("sortingColumnName");
            set => _scenarioContext.Set(value, "sortingColumnName");
        }
        
        public int SortedColumnIndex
        {
            get => _scenarioContext.Get<int>(key: "sortedAttributeIndex");
            set => _scenarioContext.Set(value, key: "sortedAttributeIndex");
        }

        public string SortedColumnName
        {
            get => _scenarioContext.Get<string>(key: "sortingColumnName");
            set => _scenarioContext.Set(value, key: "sortingColumnName");
        }

        [When(@"Click Create New Reasons Button")]
        public void ClickCreateReturnReasonButton()
        {
            ReturnReasonsPage.CreateNewReasonButton.Click();
        }

        [Then(@"Create New Reasons modal is opened")]
        public void ReturnReasonsModalIsOpened()
        {
            ReturnReasonsCreateModal.WaitForModalAppeared();
        }
        
        [When(@"Fill All fields correctly")]
        public void FillAllField()
        {
            var randomCode = Randomizer.GetRandomString(length: 4);
            var randomReason = Randomizer.GetRandomString(length: 30);

            ReturnReasonsCreateModal.CodeNameInput.ClearAndSendKeys(randomCode);
            ReturnReasonsCreateModal.ReasonsInput.ClearAndSendKeys(randomReason);
        }

        [When(@"Open Return Reason with (EFT|Chargeback Credit) type")]
        public void AndOpenReturnReasonWithEFTType(string searchName)
        {
            ReturnReasonsPage.SearchReasonFieldInput.ClearAndSendKeys(searchName);
        }

        [When(@"I sort results by random column ascending in a Return Reasons block")]
        public void SortColumnAsc()
        {
            var  columnName = EnumExtensions.GetRandomValue<ReturnReasonsTableColumnNameEnum>();
            SortingColumnIndex = (int)columnName;
            ReturnReasonsPage.SortByColumnNameAsc(columnName);
            SortingColumnName = ReturnReasonsPage.TableColumnHeaders.ElementAt(SortingColumnIndex).Text;
        }
        
        [When(@"I sort result by random column descending in a Return Reasons block")]
        public void SortColumnDesc()
        {
            var  columnName = EnumExtensions.GetRandomValue<ReturnReasonsTableColumnNameEnum>();
            SortedColumnIndex = (int)columnName;
            ReturnReasonsPage.SortByColumnNameDesc(columnName);
            SortedColumnName = ReturnReasonsPage.TableColumnHeaders.ElementAt(SortedColumnIndex).Text;
        }

        [Then(@"Results should be sorted ascending in a Return Reasons block")]
        public void CheckSortingAsc()
        {
            var list = ReturnReasonsPage.GetSortedAttributesList(SortingColumnName);
            list.Should().BeInAscendingOrder();
        }
        
        [Then(@"Result should be sorted descending in a Return Reasons block")]
        public void CheckSortingDesc()
        {
            var list = ReturnReasonsPage.GetSortedAttributesList(SortingColumnName);
            list.Should().BeInDescendingOrder();
        }

        [Then(@"Click Edit Button")]
        public void ClickEditButton()
        {
            ReturnReasonsPage.EditButton.Click();
        }

        [Then(@"Check (main|random) pagination block")]
        public void CheckPaginationBlock(string namePagination)
        {
            var paginationBlock = ReturnReasonsPaginationSettings;
            switch (namePagination)
            {
                case "main":
                    paginationBlock.PaginationNextPage.IsEnabled.Should().BeTrue();
                    paginationBlock.PaginationLastPage.IsEnabled.Should().BeTrue();
                    paginationBlock.PaginationInfoText.Text.Should().Contain("1 - 25");
                    break;
                
                case "random":
                    var randomNameReason = Randomizer.GetRandomString(length: 10);
                    ReturnReasonsPage.SearchReasonFieldInput.ClearAndSendKeys(randomNameReason);
                    paginationBlock.PaginationInfoText.Text.Should().Contain("No items to display");
                    break;
                
                default:
                    throw new NotSupportedException();
            }
        }

        [When(@"Update fields with new data for (EFT|Chargeback Credit) type")]
        public void AndUpdateFieldsWithNewDataForType(string typeUpdate)
        {
            var updatedReturnReasonsCode = Randomizer.GetRandomString(length: 4);
            var updatedReturnReasonsField = Randomizer.GetRandomString(length: 30);
            ReturnReasonsCreateModal.ReasonTypeSelectDisabled.IsDisabled();

            switch (typeUpdate)
            {
                case "EFT":
                    ReturnReasonsCreateModal.CodeNameInput.ClearAndSendKeys(updatedReturnReasonsCode);
                    ReturnReasonsCreateModal.ReasonsInput.ClearAndSendKeys(updatedReturnReasonsField);
                    ReturnReasonsCreateModal.EFTReturnActionSelect.SetRandomElement();
                    break;
                
                case "Chargeback Credit":
                    ReturnReasonsCreateModal.CodeNameInput.ClearAndSendKeys(updatedReturnReasonsCode);
                    ReturnReasonsCreateModal.ReasonsInput.ClearAndSendKeys(updatedReturnReasonsField);
                    break;
                
                default:
                    throw new NotSupportedException();
            }
        }

        [When(@"Select (Card|Chargeback Credit|EFT) reason type")]
        public void SelectReasonType(string typeName)
        {
            ReturnReasonsCreateModal.ReasonTypeSelect.SetElementContainingText(typeName);
        }

        [When(@"Select (Block Account After EFT Return Threshold|Block Account Immediately|Do not Block Account) EFT Return Action type")]
        public void SelectEFTReturnActionType(string EFTActionType)
        {
            ReturnReasonsCreateModal.EFTReturnActionSelect.SetElementContainingText(EFTActionType);
        }

        [When(@"Select random category")]
        public void SelectRandomCategory()
        {
            ReturnReasonsCreateModal.ReturnCategorySelect.SetRandomElement();
        }

        [Then(@"I should see Success Return Reason added notification is appeared")]
        public void IShouldSeeSuccessReturnReasonAddedNotificationIsAppeared()
        {
            ReturnReasonsPage.Notification.IsTextOfNotificationContains(
                true,
                ResponseCodes.SuccessResponse,
                "Reasons was created").Should().BeTrue();
        }

        [Then(@"I should see Success updated Return Reasons notification is appeared")]
        public void IShouldSeeSuccessUpdatedReturnReasonsNotificationIsAppeared()
        {
            ReturnReasonsPage.Notification.IsTextOfNotificationContains(
                true,
                ResponseCodes.SuccessResponse,
                "Reason was updated").Should().BeTrue();
        }

        [Then(@"Wait until the confirmation window disappears")]
        public void WaitUntilTheConfirmationWindowDisappears()
        {
            ReturnReasonsPage.Notification.WaifForNotificationDisappeared()(); 
        }

        [When(@"Click Save button")]
        public void ClickSaveButton()
        {
            ReturnReasonsCreateModal.SaveReasonButton.Click();
        }
    }
}