@Exceptions
Feature: Developer Can Fluently Throw Alerts As Exceptions
	In order to document the current system state
	As a developer
	I want to be able to wrap an alert in an exception
	
Scenario: Can throw an alert exception with an internal alert
	Given I have an filled alert builder
	 When I throw the alert
	 Then the exception is an alert exception
	  And the exception contains the alert
	  And the exception's message is the alerts title

Scenario: Can throw an alert exception with an internal alert and inner exception
	Given I have an filled alert builder
	Given I have an exception
	When I throw the alert with the inner expectpion
	 Then the exception is an alert exception
	  And the exception contains the alert
	  And the exception's message is the alerts title
	  And the original exception is now the inner exception
	  	  
#@EaseOfUse
#Scenario: Can create an alert exception from simple text
#	When I create an alert exception with the text message
#	Then the exception's message is the alerts title
#	 And exception message is the simple text
#
#@EaseOfUse
#Scenario: Can create an alert exception from simple text and other exception
#   Given I have an exception
#	When I create an alert exception with text message and the inner exception
#	Then the exception's message is the alerts title
#	 And exception message is the simple text
#	 And the original exception is now the inner exception

@Extensibility
Scenario: Can throw a dervided alert exception with an internal alert and inner exception
	Given I have an filled alert builder
	 When I throw the alert as some dervied alert exception with the inner exception
	 Then the exception is of the derived alert exception type
	  And the exception contains the alert
	  And the exception's message is the alerts title
	  And the original exception is now the inner exception

@Extensibility
Scenario: Can throw a dervided alert exception with an internal alert
	Given I have an filled alert builder
	  And I have an exception
	 When I throw the alert as some dervied alert exception
	 Then the exception is of the derived alert exception type
	  And the exception contains the alert
	  And the exception's message is the alerts title

@EaseOfUse	  
Scenario: Can create an alert exception from a current builder
	Given I have an filled alert builder
	 When I create an alert exception with the builder
	 Then the exception contains the alert
	  And the exception's message is the alerts title

@EaseOfUse
Scenario: Can create an alert exception from a current builder and other exception
	Given I have an filled alert builder
	  And I have an exception
	 When I create an alert exception with a builder and other exception
	 Then the exception contains the alert
	  And the exception's message is the alerts title
	  And the original exception is now the inner exception

