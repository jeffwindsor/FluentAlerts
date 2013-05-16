Feature: Developer Can Fluently Throw Alert As An Exception
	In order to document the current system state
	As a developer
	I want to be able to throw alert wtih exceptions

Scenario: Throw an alert exception
	Given I have an filled alert builder
	 When I throw the alert
	 Then the exception is an alert exception
	  And the exception contains the alert
	  And the exception's message is the alerts title

Scenario: Throw a derived alert exception
	Given I have an filled alert builder
	 When I throw the alert as some dervied alert exception
	 Then the exception is of the derived alert exception type
	  And the exception contains the alert
	  And the exception's message is the alerts title

Scenario: Wrap a caught exception and throw an alert exception
	Given I have an exception
	When I wrap it in an alert
	 And I throw the alert
	 Then the exception is an alert exception
	  And the exception contains the alert
	  And the exception's message is the alerts title
	  And the original exception is now the inner exception

Scenario: Wrap a caught exception and throw a derived alert exception
	Given I have an exception
	 When I wrap it in an alert
	  And I throw the alert as some dervied alert exception
	 Then the exception is of the derived alert exception type
	  And the exception contains the alert
	  And the exception's message is the alerts title
	  And the original exception is now the inner exception