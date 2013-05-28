Feature: DeveloperCanSpecifyRenderTemplates

Scenario Outline: Can render alert with file based templates
	Given I have a built alert
	 And I have a <type> template render
	When I render the alert
	Then the rendered text has the <type> formatting
Examples:
	| type                   |
	| Custom                 |
	| HtmlWithEmbeddedStyles |
	| HtmlWithCss            |
	| Json                   |
	| Text                   |
	
Scenario: Can export default templates to file
	When I export the templates to a file
	Then the deafult templates are in file 

Scenario: System wil cerate a backup of the current file when exporting
	Given I set the templates location in the config
	  And I have a file at that location
	When I export the templates to a file
	Then the deafult templates are in file

@Extensibility
Scenario Outline: Can set the location of the template file in the config
	Given I set the templates location in the config
	  And I have a file at that location
	When I create a Template Dictionary
	Then the template dictionary contains <key> template
Examples:
	| key           |
	| TestTemplate1 |
	| TestTemplate2 |

@EaseOfUase
Scenario Outline: Default rendering templates are used when no template file is present
	When I create a Template Dictionary
	Then the template dictionary contains <key> template
Examples:
	| key                                    |
	| HtmlWithEmbeddedStylesTableTemplate    |
	| HtmlWithEmbeddedStylesDocumentTemplate |
	| HtmlWithCssTableTemplate               |
	| HtmlWithCssDocumentTemplate            |
	| JsonTemplate                           |
	| TextTemplate                           |

@Extensibility
Scenario: Can set the default template in the config
	Given I set the default template in the config
	When I create a default render
	Then the render uses the template in the config

@EaseOfUase
Scenario: Default rendering template is plain HTML when not specified in the config
	Given I have a built alert
	When I create a default render
	 And I render the alert
	Then the rendered text has the plain HTML formatting
		