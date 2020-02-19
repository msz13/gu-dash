Feature: Create Competence
	In order to track my competences progress
	As user - learner
	I can create competence and define habits


@mytag
Scenario: Create positively competence
	Given User of ID '1'
	And competence name 'uprzejmosc'
	When Request
	Then can see competence with properties
	| id  | name       | description      |   isRequired | numerOfActiveHabits | numberOfHoldedHabits | numerOfDoneHabits |
	| "1" | Uprzejmość |aby szanować ludzi| false        | 0                   | 0                    | 0                 | 




Scenario: Duplicate competence name

Scenario: Required Competence


Scenario: Update name


Feature: Learner browses list of competences

Feature: See competence details
Feature: 

Scenario: See in active Habits list




 
