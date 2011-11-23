Feature: A developer can include an alert in an exception
	In order to commiunicate the current system state in a human readable format
	As a developer
	I want to be able to wrap an alert in an exception

Background: 
	Given I have an alert factory

Scenario: Can create an alert exception from simple text
	Given a text message
	When I create an alert exception with the text message
	Then the exception alert contains a text block containing the text message
	 And exception message is the text message

Scenario: Can create an alert exception from a current builder
	Given a non-empty builder
	When  I create an alert exception with the builder
	Then the exception alert is the alert created from the builder

Scenario: Can create an alert exception from an alert
	Given an alert
	When I create an alert exception with the alert
	Then the exception alert is the alert
	 And the exception message is the alert serialized to text

Scenario: Can create an alert exception from simple text and other exception
	Given a text message
	 And an inner exception
	When I create an alert exception with text message and the inner exception
	Then the exception alert contains a text block containing the text message
	 And exception message is the text message
	 And inner exception is other exception

Scenario: Can create an alert exception from a current builder and other exception
	Given a non-empty builder
	 And an inner exception
	When  I create an alert exception with a builder and other exception
	Then the exception alert is the alert created from the builder
	And inner exception is other exception

Scenario: Can create an alert exception from an alert and other exception
	Given an alert
	 And an inner exception
	When  I create an alert exception with an alert and other exception
	Then the exception alert is the alert
	 And the exception message is the alert serialized to text
	 And inner exception is other exception