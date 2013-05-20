@Serialization
Feature: Developer Can Specify Transformations Options
	In order to specify the information that is serialized
	As a developer
	I want to be able to select the properties, methods and fields that will be serialized by type and at depth

	#depth
	#path

Scenario: Default transformer limits depth to zero for all non exception classes
	Given I have a object graph of depth 5
	  And I have a HeirarchialNameValueRowTransformer with a DefaultTypeInformer
	 When I tranform the object
	 Then the transformer does not create any sub alerts

Scenario: Default transformer does not limit depth for all exception classes

@UnitTest
Scenario: Can specify transformation depth of an object to base properties
	Given I have a object graph of depth 5
	  And I have a HeirarchialNameValueRowTransformer with a DefaultTypeInformer

Scenario: Can specify transformation depth of a type
	Given I have a object graph of depth 5
      And I have a HeirarchialNameValueRowTransformer with a DefaultTypeInformer

#exception scenario
Scenario: Can specify transformation depth of a type be unlimited
	Given I have a object graph of <n> depth
	
	
Scenario: Defaults to selecting all properties and fields but no methods
#declared and runtime types?

@Extensibility
Scenario: Can limit type selections
	Given inconclusive 

@Extensibility
Scenario: Can add to type selections
	Given inconclusive 

@Extensibility
Scenario: Can limit global selections
	Given inconclusive 

@Extensibility
Scenario: Can add to global selections
	Given inconclusive 
