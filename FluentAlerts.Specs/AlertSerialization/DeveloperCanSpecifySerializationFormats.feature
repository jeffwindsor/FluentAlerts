Feature: A developer can serialize an alert into multiple formats

#bracketed formatted : html, text, json, xml
#open closed - inject serializer into ToSerializadString(). 
#hooks

Scenario: Can create a table from an objects property values
	Given I create a table with an object using depth 0
	When I build the alert
	Then the title is the objects type
	 And a row is added for each property with cells for property name and property value

Scenario: Can create a table from an objects property values recursing reference types except strings to a given depth
	Given I create a table with an object using depth 3
	When I build the alert
	Then the title is the objects type
	 And a row is added for each property with cells for property name and property value

Scenario: Can create a table from an exception
	Given I create a table with an exception
	When I build the alert
	Then the table should have a row