@FutureFeatures
Feature: FutureFeatures

Scenario: Can config default type informer
	Given inconclusive 

Scenario: Can config default formatter
	Given inconclusive 

 
#@Exceptions @EaseOfUse
#Feature: DeveloperCanSpecifyWhatHappensAtPointOfFailure
Scenario: Render Template File not found
	Given inconclusive 

Scenario: Render Template File not valid
	Given inconclusive 
	
Scenario: Cyclic references
	Given inconclusive 

Scenario: Obtain value failure
	Given inconclusive 

Scenario: No info for type
	Given inconclusive 

Scenario: Null value
	Given inconclusive 
	
Scenario Outline: Can limit transformation of properties by path
	Given  I have custom app settings
	Given I have a default type informer
	 And I have a NestedTestClass object
	 And I limit the informer to <type> properties at <path> 
	When I get the object's type info
	Then only the objects <type> properties at <path> are listed in the type info
Examples:
	| type       | path                           |
	| DateTime   | NestedTestClass.Child.TestDate |
	| NumberEnum | NestedTestClass.TestNumber     |

Scenario Outline: Can limit transformation of fields by path
	Given  I have custom app settings
	Given I have a default type informer
	 And I have a NestedTestClass object
	 And I limit the informer to <type> properties at <path> 
	When I get the object's type info
	Then only the objects <type> fields at <path> are listed in the type info 
Examples:
	| type       | path                       |
	| DateTime   | NestedTestClass.Child.Date |
	| NumberEnum | NestedTestClass.Number     |