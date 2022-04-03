@UI
@using_UI
@GeneratedProcessOneData
@ReturnReasons

Feature: ReturnReasonsPage
	Check work  Return Reasons block in Unicorn UI
	
	Background: 
		Given Create Admin Unicorn Test User
		And I log in to ProcessOne as test user
		And Open Return Reasons page

@C27691980
Scenario: Create New Reasons for type = Card
	When Click Create New Reasons Button
	Then Create New Reasons modal is opened
	When Fill All fields correctly
	And Select Card reason type
	And Select random category
	And Click Save button
	Then I should see Success Return Reason added notification is appeared
	
@C27691974
Scenario: Create New Reasons for type = Chargeback Credit
	When Click Create New Reasons Button
	Then Create New Reasons modal is opened
	When Fill All fields correctly
	And Select Chargeback Credit reason type
	And Click Save button
	Then I should see Success Return Reason added notification is appeared
	
@C27690624
Scenario: Create New Reason for type = EFT
	When Click Create New Reasons Button
	Then Create New Reasons modal is opened
	When Fill All fields correctly
	And Select EFT reason type
	And Select Block Account After EFT Return Threshold EFT Return Action type
	And Click Save button
	Then I should see Success Return Reason added notification is appeared
	And Wait until the confirmation window disappears
	When Click Create New Reasons Button
	Then Create New Reasons modal is opened
	When Fill All fields correctly
	And Select EFT reason type
	And Select Block Account Immediately EFT Return Action type
	And Click Save button
	Then I should see Success Return Reason added notification is appeared
	And Wait until the confirmation window disappears
	When Click Create New Reasons Button
	Then Create New Reasons modal is opened
	When Fill All fields correctly
	And Select EFT reason type
	And Select Do not Block Account EFT Return Action type
	And Click Save button
	Then I should see Success Return Reason added notification is appeared
	
@C27691975
Scenario: Create New Reason for type = Undefined
	When Click Create New Reasons Button
	Then Create New Reasons modal is opened
	When Fill All fields correctly
	And Click Save button
	Then I should see Success Return Reason added notification is appeared
	
@C27690626
Scenario: Update Return Reasons
	When Open Return Reason with EFT type
	Then Click Edit Button
	When Update fields with new data for EFT type
	And Click Save button
	Then I should see Success updated Return Reasons notification is appeared
	When Open Return Reason with Chargeback Credit type
	Then Click Edit Button
	When Update fields with new data for Chargeback Credit type
	And Click Save button
	Then I should see Success updated Return Reasons notification is appeared
	
@C27690936
Scenario: General Page Check
	When I sort results by random column ascending in a Return Reasons block
	Then Results should be sorted ascending in a Return Reasons block
	When I sort result by random column descending in a Return Reasons block
	Then Result should be sorted descending in a Return Reasons block
	And Check main pagination block
	And Check random pagination block

	
	