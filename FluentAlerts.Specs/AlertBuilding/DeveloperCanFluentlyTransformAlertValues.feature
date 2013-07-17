@Alerts
Feature: DeveloperCanFluentlyTransformAlertValues

@EaseOfUse
Scenario: Can fluently transform and format alert values to strings using a custom transformer
	Given I have an filled alert builder
	  And I have a default transformer
	When I build the alert
	 And I transform the alert using a transformer
	Then there should not be any element that is not a graph class or result type