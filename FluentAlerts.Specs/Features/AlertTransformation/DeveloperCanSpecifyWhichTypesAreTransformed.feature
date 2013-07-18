@Transformation
Feature: DeveloperCanSpecifyWhichTypesAreTransformed 
	In order to have full control over the alert output
	As a developer
	I want to be able to specify what types are transformed

Scenario: Default transformer creates a default alert from an object
	Given I have a default transformer
	 And I have a NestedTestClass object
	When I transform the object
	Then the result should be an IAlert
	 And the alert title equals the object's type name
	 And the alert has a group for each of objects properties with name value pairs
	 And the alert has a group for each of objects fields with name value pairs

Scenario Outline: Transformer will not alter IAlert, IAlertItems or Result types
	Given I have an filled alert builder
	When I build the alert
	 And I transform the alert
	Then all the <type> are the same
Examples: 
	| type       |
	| IAlert     |
	| IAlertItem |
	| Result     |

Scenario: Name Type Value transformer creates a default alert from an object
	Given I have a name type value pair transformer
	 And I have a NestedTestClass object
	When I transform the object
	Then the result should be an IAlert
	 And the alert title equals the object's type name
	 And the alert has a group for each of objects properties with name type value pairs
	 And the alert has a group for each of objects fields with name type value pairs