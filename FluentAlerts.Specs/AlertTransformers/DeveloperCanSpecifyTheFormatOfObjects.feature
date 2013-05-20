Feature: DeveloperCanSpecifyTheFormatOfObjects
	In order to have full control over the alert output
	As a developer
	I want to be able to specify the format of any object during transformation

Scenario Outline: Default formatter uses the objects to string on all types
	Given I have a <type> object
	 And I have the default formatter
	When I format the object
	Then the result is equal to <type> to string
Examples:
	| type             |
	| Null             |
	| String           |
	| DateTime         |
	| Integer          |
	| Long             |
	| Float            |
	| Double           |
	| NumberEnum       |
	| NestedTestClass  |
	| NestedTestStruct |

Scenario Outline: Default formatter uses the objects to string as a title on all types
	Given I have a <type> object
	 And I have the default formatter
	When I format the object as a title
	Then the result is equal to <type> types name
Examples:
	| type             |
	| Null             | 
	| String           |
	| DateTime         |
	| Integer          |
	| Long             | 
	| Float            |
	| Double           |
	| NumberEnum       |
	| NestedTestClass  |
	| NestedTestStruct | 

Scenario Outline: Can specify string format by type
	Given I have a <type> object
	 And I have the default formatter
	 And I insert a format for the <type> at the beginning
	When I format the object
	Then the result is equal to <type> to format
Examples:
	| type             |
	| Null             | 
	| String           |
	| DateTime         |
	| Integer          |
	| Long             |
	| Float            |
	| Double           |
	| NumberEnum       |
	| NestedTestClass  |
	| NestedTestStruct | 

Scenario Outline: WARNING Can hide string format behind more general rule
	Given I have a <type> object
	 And I have the default formatter
	 And I add a format for the <type>
	When I format the object
	Then the result is equal to <type> to string
Examples:
	| type             |
	| Null             | 
	| String           |
	| DateTime         |
	| Integer          |
	| Long             |
	| Float            |
	| Double           |
	| NumberEnum       |
	| NestedTestClass  |
	| NestedTestStruct | 

#ie money format for Request.Group.Account.Value, but no other decimal
Scenario Outline: Can specify string format by path
	Given I have a <type> object
	 And I have a formatter
	 And I specify a format for the <type> at A.B.C
	When I format the object at A.B.C 
	Then the result is equal to <type> to format
Examples:
	| type             |
	| Null             | 
	| String           | 
	| DateTime         |
	| Integer          |
	| Long             |
	| Float            |
	| Double           |
	| NumberEnum       |
	| NestedTestClass  |
	| NestedTestStruct | 

Scenario Outline: Specifing string format by path does not format type at other paths
	Given I have a <type> object
	 And I have a formatter
	 And I specify a format for the <type> at A.B.C 
	When I format the object at A.B.D
	Then the result is equal to <type> to string
Examples:
	| type             |
	| Null             | 
	| String           |
	| DateTime         |
	| Integer          |
	| Long             |
	| Float            |
	| Double           |
	| NumberEnum       |
	| NestedTestClass  |
	| NestedTestStruct | 