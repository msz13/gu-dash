Feature: Define and update competences
In order to recod progress of competences
As learner or competencey buidler
I can define and update competences which I try to develop

Background: 
Given logged user Mat Szczeciński of ID "facebook-1234567"


Scenario: Define competence with succes
When Mat defines competence with values
| Name       | Description           |
| Uprzejmość | Uprzejmość na codzień |
Then competence is created
And He can see competence with values
| Name       | Description           | IsRequired | IsActive | NumberOfActiveHabits | NumberOfHoldedHabits | NumberOfDoneHabits |
| Uprzejmość | Uprzejmość na codzień | false      | false    | 0                    | 0                    | 0                  |


Scenario: Define competence with duplicate name 
Given Defined competence with values
| Name        | Description           |
| Komunikacja | Komunikacja w pracy |
When Mat defines competence with the same name
Then competence is not created and he can see error message with competence name "Uprzejmość"

Scenario: Rename competence

Scenario: Rename competence with duplicate name

Scenario: Change description

Scenario: Unauthrized user update competence

Scenario: Define competence with closed user account

Scenario: Update competence with closed user account

Scenario: Define competence with invalid inputs




