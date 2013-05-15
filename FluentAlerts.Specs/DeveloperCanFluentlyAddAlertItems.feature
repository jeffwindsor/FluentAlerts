Feature: A developer can fluently add documenation times to an alert
	In order to document the current system state
	As a developer
	I want to be able to add items to an alert
	
Scenario: Can add a title
	Given I have an alert builder
	When I add a title
	 And I build the alert
	Then the alert should contain title as the first item

Scenario: Can add a format based title 
	Given I have an alert builder
	When I add a format based title
	 And I build the alert
	Then the alert should contain title as the first item

Scenario: Can add text
	Given I have an alert builder
	When I add Normal text
	 And I build the alert
	Then the alert should contain Normal text as the last item

Scenario: Can add a format based text
	Given I have an alert builder
	When I add Normal text
	 And I build the alert
	Then the alert should contain Normal text as the last item

Scenario Outline: Can add a styled text
	Given I have an alert builder
	When I add <text_style> text
	 And I build the alert 
	Then the alert should contain <text_style> text as the last item
Examples:
	| text_style |
	| Normal     |
	| Bold       |
	| Header     |
	| Footer     |

Scenario Outline: Can add a format based styled text
	Given I have an alert builder
	When I add <text_style> text
	 And I build the alert
	Then the alert should contain <text_style> text as the last item
Examples:
	| text_style |
	| Normal     |
	| Bold       |
	| Header     |
	| Footer     |

Scenario: Can add a seperator
	Given I have an alert builder
	When I add a seperator
	 And I build the alert
	Then the alert should contain a seperator as the last item

Scenario: Can add a url
	Given I have an alert builder
	When I add a url
	 And I build the alert
	Then the alert should contain a url as the last item with the url and text

Scenario: Can add an object
	Given I have an alert builder
	When I add an object
	 And I build the alert
	Then the alert should contain that object as the last item

Scenario: Can add a list of objects
	Given I have an alert builder
	When I add a list of object
	 And I build the alert
	Then the alert should contain each object in order

Scenario: Can add an alert
	Given I have an alert builder
	When I add another alert
	 And I build the alert
	Then the alert should contain all the other alert's items

Scenario: Can add rows
	Given I have an alert builder
	When I add a row
	 And I build the alert
	Then the alert should contain that 'Normal' row as the last item

Scenario Outline: Can add styled rows
	Given I have an alert builder
	When I add a <row_style> row
	 And I build the alert
	Then the alert should contain that <row_style> row as the last item
Examples:
	| row_style |
	| Normal    |
	| Highlight |