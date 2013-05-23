﻿@Transformation
Feature: DeveloperCanSpecifyTypeMemberToTransform
	In order to have full control over the alert output
	As a developer
	I want to be able to specify the properties and fields which will be transformed
	
Scenario: Can config default type informer
	Given inconclusive 

Scenario: Defaults to selecting all properties and fields
	Given I have a default type informer
	 And I have a NestedTestClass object
	When I get the object's type info
	Then all the objects properties are listed in the type info
	 And all the objects fields are listed in the type info

Scenario Outline: Can limit transformation of type properties
	Given I have a default type informer
	 And I have a NestedTestClass object
	 And I limit the informer to <type> properties  
	When I get the object's type info
	Then only the objects <type> properties are listed in the type info
	Examples:
	| type             |
	| DateTime         |
	| NumberEnum       |

Scenario Outline: Can limit transformation of properties by path
	Given I have a default type informer
	 And I have a NestedTestClass object
	 And I limit the informer to <type> properties at <path> 
	When I get the object's type info
	Then only the objects <type> properties at <path> are listed in the type info
Examples:
	| type       | path                           |
	| DateTime   | NestedTestClass.Child.TestDate |
	| NumberEnum | NestedTestClass.TestNumber     |

Scenario Outline: Can limit transformation of type fields
	Given I have a default type informer
	 And I have a NestedTestClass object
	 And I limit the informer to <type> fields  
	When I get the object's type info
	Then only the objects <type> fields are listed in the type info
Examples:
	| type       | 
	| DateTime   | 
	| NumberEnum |

Scenario Outline: Can limit transformation of fields by path
	Given I have a default type informer
	 And I have a NestedTestClass object
	 And I limit the informer to <type> properties at <path> 
	When I get the object's type info
	Then only the objects <type> fields at <path> are listed in the type info 
Examples:
	| type       | path                       |
	| DateTime   | NestedTestClass.Child.Date |
	| NumberEnum | NestedTestClass.Number     |