Feature: A developer can create an alert
	In order to document the current system state
	As a developer
	I want to be able to create an alert of current state

Scenario: Can create an alert with a title
	Given I have an alert builder and a title
	When I build the alert
	Then the alert should contain title as the first item
	
Scenario: Can create an alert
	Given I have an alert builder
	When I build the alert 
	Then the alert should be empty
	 And the alert should be a list of alert items
