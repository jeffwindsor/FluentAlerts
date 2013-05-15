Feature: A developer can create a document like alert
	In order to document the current system state
	As a developer
	I want to be able to create an alert of current state

Scenario: Can create an alert with a title
	Given I have an alert builder and a title
	When I build the alert
	Then the alert should contain title as the first item
	
Scenario: Can create an alert
	Given I have an alert builder
	When I build the alert 
	Then the alert should be empty
	 And the alert should be a list of alert items

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
	When I add <style> text
	 And I build the alert 
	Then the alert should contain <style> text as the last item
Examples:
	| style  |
	| Normal |
	| Bold   |
	| Title  |

Scenario Outline: Can add a format based styled text
	Given I have an alert builder
	When I add <style> text
	 And I build the alert
	Then the alert should contain <style> text as the last item
Examples:
	| style  |
	| Normal |
	| Bold   |
	| Title  |

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
	Then the alert should contain the other alert as the last item