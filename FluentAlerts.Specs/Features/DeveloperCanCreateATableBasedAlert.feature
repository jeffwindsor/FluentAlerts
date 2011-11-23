Feature: A developer can create a table based alert
	In order to document the current system state in a human readable format
	As a developer
	I want to be able to create a hierarchical table of information
	 
Background: 
	Given I have an alert factory

Scenario: Can add normal rows to a table
	Given I have a table builder
	When I add the 'normal' rows
		| Cell1 | Cell2 |
		| One   | 1     |
		| Two   | 2     |
	 And I build the alert
	Then the alert should have 2 rows
	 And row 1 should be a 'normal' row
	 And row 1 cell 1 should be 'One'
	 And row 1 cell 2 should be '1'
	 And row 2 should be a 'normal' row
	 And row 2 cell 1 should be 'Two'
	 And row 2 cell 2 should be '2'

Scenario: Can add highlighted rows to a table
	Given I have a table builder
	When I add the 'highlighted' rows
		| Cell1 | Cell2 |
		| One   | 1     |
	 And I build the alert
	Then the alert should have 1 rows
	 And row 1 should be a 'highlighted' row
	 And row 1 cell 1 should be 'One'
	 And row 1 cell 2 should be '1'

Scenario: Can add header rows to a table
	Given I have a table builder
	When I add the 'header' rows
		| Cell1 |
		| One   |
	 And I build the alert
	Then the alert should have 1 rows
	 And row 1 should be a 'header' row
	 And row 1 cell 1 should be 'One'
	 And row 1 should have 1 cells

Scenario: Can add footer rows to a table
	Given I have a table builder
	When I add the 'footer' rows
		| Cell1 |
		| One   |
	 And I build the alert
	Then the alert should have 1 rows
	 And row 1 should be a 'footer' row
	 And row 1 cell 1 should be 'One'
	 And row 1 should have 1 cells

Scenario: Can create a table with a title
	Given I create a table with a title 'some title'
	When I build the alert
	Then the alert should have 2 columns
	 And the alert should have 1 rows
	 And row 1 should be a 'header' row 
	 And row 1 should have 1 cells
	 And row 1 cell 1 should be 'some title'

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
	Then the table should have a row for the type, message and stack trace for all exception in chain