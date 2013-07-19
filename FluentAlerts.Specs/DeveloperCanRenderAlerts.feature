@Rendering
Feature: DeveloperCanRenderAlerts

Scenario: Can render an alert
	Given I have a full test alert
	 And I import the render templates
	 And I have a OutlookEmailCompliantTemplate template
	 And I have a template render
	 And I have a default transformer
	 And I have an alert render
	When I render the alert
	Then the rendered text has the default formatting

@Extensibility
Scenario: Can specify the default template used to render alerts
	Given I have custom app settings
	 And I set the default template name to TestTemplate
	 And I import the render templates
	When I get the default template
	Then the template is the TestTemplate

@Extensibility
Scenario: Can specify the templates source file used for template choices
	Given I have custom app settings
	 And I set the default template file location to custom_templates.json
	 And I have a template file at custom_templates.json
	 And I have a template issue handler
	When I get the template choices from the default file
	 And I create a new template dictionary from custom_templates.json
	Then the template dictionaries are equivilant
	 And clean up file custom_templates.json

@EaseOfUse
Scenario: Html with Embedded Css template is used when no default template name is specified
	Given I have the default app settings
	Then the settings default template name is the Html with Embedded Css template 

@EaseOfUse
Scenario: Default rendering templates are used when no template file is present
	Given I have the default app settings
	 And I have a template issue handler
	When I get the template choices from the default file
	 And I create a new template dictionary from Default
	Then the template dictionaries are equivilant

@Extensibility
Scenario: Can import templates from files
   Given I have a template issue handler
	When I import the templates
	Then the template dictionaries contains all the local templates

Scenario: Can export templates to files with backups
   Given I have a template issue handler
	When I import the templates
	 And delete all local template files
	 And I export the templates
	Then the template dictionaries contains all the local templates
	
@Extensibility @EaseOfUse
Scenario: System will create a backup of the current file when exporting
	Given I have custom app settings
	 And I have a template issue handler
	 And I have the template choices from the default file
	 And I have a template file at back_test.json
	When I export the templates to back_test.json
	Then a backup of the original back_test.json is written to the same directory
	 And clean up file back_test.json
		