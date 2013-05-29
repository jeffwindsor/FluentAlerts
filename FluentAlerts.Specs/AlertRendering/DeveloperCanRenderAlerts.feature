@Rendering
Feature: DeveloperCanRenderAlerts

Scenario: Can render an alert
	Given I have a built alert
	 And I have a default alert render
	When I render the alert
	Then the rendered text has the default formatting

@Extensibility
Scenario: Can specify the template used to render alerts
	Given I set the default template name to TestTemplate
	 And I have the template choices from the default file
	When I get the default template
	 And I get the TestTemplate as the other template
	Then the templates are equivilant

@EaseOfUse
Scenario: Html with Embedded Css template is used when no default template name is specified
	Given I have the default app settings
	Then the settings default template name is the Html with Embedded Css template 

@Extensibility
Scenario: Can specify the templates source file used to create template choices
	Given I set the default template file location to custom_templates.json
	 And I have a template file at custom_templates.json
	When I get the template choices from the default file
	 And I create a new template dictionary from custom_templates.json
	Then the template dictionaries are equivilant
	 And clean up file custom_templates.json

@EaseOfUse
Scenario: Default rendering templates are used when no template file is present
	Given I have the default app settings
	When I get the template choices from the default file
	 And I create a new template dictionary from Default
	Then the template dictionaries are equivilant

@Extensibility
Scenario: Can export and import templates to and from files
	Given I have the default app settings
	 And I have the template choices from the default file
	When I export the templates to export.json
	 And I create a new template dictionary from export.json
	Then the template dictionaries are equivilant
	 And clean up file export.json

@Extensibility @EaseOfUse
Scenario: System will create a backup of the current file when exporting
	Given I have custom app settings
	 And I have the template choices from the default file
	 And I have a template file at back_test.json
	When I export the templates to back_test.json
	Then a backup of the original back_test.json is written to the same directory
	 And clean up file back_test.json
		