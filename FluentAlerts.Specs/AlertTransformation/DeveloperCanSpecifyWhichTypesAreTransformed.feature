@Transformation
Feature: DeveloperCanSpecifyWhichTypesAreTransformed 
	In order to have full control over the alert output
	As a developer
	I want to be able to specify what types are transformed

Scenario: Default transformer uses type name as title
	Given I have a default transformer
	 And I have a NestedTestClass object
	When I transofrm the object
	Then I the alert title equals the object's type name
	
#Scenario: Default transformer limits depth to zero for all non exception classes
#	Given I have a object graph of depth 5
#	 And I have a HeirarchialNameValueRowTransformer with a DefaultTypeInformer
#	When I tranform the object
#	Then the transformer does not create any sub alerts
#
#Scenario: Default transformer does not limit depth for all exception classes
#
#@UnitTest
#Scenario: Can specify transformation depth of an object to base properties
#	Given I have a object graph of depth 5
#	 And I have a HeirarchialNameValueRowTransformer with a DefaultTypeInformer
#
#Scenario: Can specify transformation depth of a type
#	Given I have a object graph of depth 5
#     And I have a HeirarchialNameValueRowTransformer with a DefaultTypeInformer
#
##exception scenario
#Scenario: Can specify transformation depth of a type be unlimited
#	Given I have a object graph of <n> depth
#		
#
#@Extensibility
#Scenario: Can limit type selections
#	Given inconclusive 
#
#@Extensibility
#Scenario: Can add to type selections
#	Given inconclusive 
#
#@Extensibility
#Scenario: Can limit global selections
#	Given inconclusive 
#
#@Extensibility
#Scenario: Can add to global selections
#	Given inconclusive 
