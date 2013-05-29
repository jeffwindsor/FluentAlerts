@Alerts
Feature: DeveloperCanFluentlyTransformAlertValues

Scenario: Can fluently transform and fomrat alert values to strings
	Given I have an filled alert builder
	When I build the alert
	 And I transform the alert
	Then there should not be any element that is not a grpah class or result type

Scenario: Can fluently transform and format alert values to strings using a custom transformer
	Given I have an filled alert builder
	When I build the alert
	 And I transform the alert using a custom transformer
	Then there should not be any element that is not a grpah class or result type