@Serialization
Feature: DeveloperCanSpecifyDepthOfObjectGraphSerializationByType

#Request.Groups.Accounts
Scenario: Can specify serialization go no deeper than type member chain
#defaulting methods like ToString or TypeName...

#Any Request object will not serialize 
Scenario: Can specify serialization go no deeper than type