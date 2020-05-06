Feature: Habits creation
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers


Scenario: Create holded habit
	Given Mat defines competence with name Uprzejmość
	When Mat defines habit with values
	|Name     |  Description |TargetType|TagetValue|
	|Witać się|Z nieznajomymi|Checkbox  |1         |
	Then Mat can see new habit with values
	| Name      | Description    | Competence | TargetType | TagetValue | History |
	| Witać się | Z nieznajomymi | Uprzejmość | Checkbox   | 1          | Null    |
	And Competence has values
	| Number of active habits | Number of holded habits | Number of done habits |
	| 0					      | 1                       | 0                     |

Scenario:  Create holded habit with no competence
	When Mat defines habit with values
	|Name     | Description |TargetType|TagetValue|
	|Witać się|Z nieznajomymi|Checkbox  |1         |
	Then Mat can see new habit with values
	| Name      | Description    | Competence   | TargetType | TagetValue | History |
	| Witać się | Z nieznajomymi | No competence| Checkbox   | 1          | Null    |

Scenario:  Cant' create habit with duplicate name 

Scenario: Create active habit with success
	Given Mat defines competence with name Sprawność
	And On day 2020 04 01
	When Mat defines active habit with values
	| Name      | Description    | TargetType | TagetValue |
	| Witać się | Z nieznajomymi | Checkbox   | 1          |
	Then Habit is created 
	And He can see habit with values
	| Name      | Description    | Competence    | Target Type | Taget Value | Start active | Days on target | Progress on last day | History |
	| Witać się | Z nieznajomymi | No competence | Checkbox    | 1           | 2020 04 01   | 0              | null                 | null    |
	And Competence has values
	| Number of active habits | Number of holded habits | Number of done habits |
	| 1                       | 0                       | 1                     |


Scenario: Can't add active habit because of reached user plan limit
	Given Mat with account plan with max numer of active habits 2
	And Two created active habits
	When Mat defines active habit
	Then Holded habit is created and he can see error message: You reached max numer of active habits: 2. Upgrade your plan

	

Scenario: Can't add active habit because reqired competence is defined and not activated
	Given Mat defined competence "Uprzejmość" with required status
	And Competence "Sprawność"
	When Mat defines active habit on competence "Sprawność"
	Then Holded habit is created and he can see error message: You have required competence Uprzejmość, but you didn't defined habit.

Scenario: Can't add active habit because of competence is marked as required and not activated
	Given Mat defined competence "Uprzejmość" with required status
	And Competence "Sprawność"
	When Mat defines active habit on competence "Sprawność"
	Then Holded habit is created and he can see error message: You have required competence Uprzejmość, but you didn't defined habit.

	
Scenario: Can't add active habit becouse of inactive reqired competence and reached user plan limit
	Given Mat with account plan with max numer of active habits 2
	And Mat defined competences
	| Name       | Description           | IsRequired |
	| Uprzejmość | Uprzejmość na codzień | true       |
	| Sprawność  | ""                    | false      |
	And defined 2 active habits
	When When Mat defines active habit "Bieg rano" on competence "Sprawność"
	Then Holded habit is created but can see error message: You reached max numer of active habits: 2. Please upgrade your plan. Habit "Bieg rano" was created as holded.

Scenario: Activate holded habit with succes
	Given Mat defined competence "Uprzejmość"
	And And Mat defined holded habit 
	When Mat activate habit
	Then Habit is activated
	And Competence has 
	| Number of active habits | Number of holded habits | Number of done habits |
	| 1                       | 0                       | 1                     |

Scenario Outline: Cant'activate habit becouse of inactive reqired competence and reached user plan limit
	Given Mat with account plan with max numer of active habits 2
	And Mat defined competences
	| Name       | Description           | IsRequired |
	| Uprzejmość | Uprzejmość na codzień | true       |
	| Sprawność  | ""                    | false      |
	And defined 2 active habits
	And And Mat defined holded habit
	When Mat activates habit
	Then Habit is not activated and he can see error message: You reached max numer of active habits: 2. Please upgrade your plan. 

Scenario Outline: Can't activate habit after user account plan changed
	Given Mat has accaunt plan with max two artive habits
	And Mat has two active habits
	And User upgraded account plan and have max 5 active habits
	When Mat <adds active habit>
	Then Habit is activated
	Examples: 
	| add active habit         |
	| defines active habits    |
	| activates existing habit |


Scenario: Change number of active habits after user account plan downgraded

Scenario: Start active day depends on user time zone

Scenario: End active day after user time zoned changed

Scenario: Mark active habit as done
	Given Defined competence "Uprzejmość"
	And Activete habit at date 2020-04-04
	And With 3 days on target  
	When Mat mark habit as done at date 2020-04-08
	Then He can see inactive habit with values
	| status |
	| DONE   |
	
	And Habit history contains
	| start active | end active | number of days ona target |
	| 2020-04-04   | 2020-04-08 |???
	And Competence has values
	| Number of active habits | Number of holded habits | Number of done habits |
	| 1                       | 0                       | 1                     |



Scenario: Mark active habit as holded

Scenario: Mark holded habit as done

Scenario: Can't mark holded habit as done with no progress hostory

Scenario: Delete habit

Scenario: Delete competece
	Given Defined competence "Wiedza specjalistyczna"
	When  Mat deletes this competence
	Then  Competence is deleted
	And When he query competence he can see error message "Competence Wiedza specjalistyczna not found"

Scenario Outline: Update habit progress with checkbox target
	Given Habit with chackboxtype
	When Mat updates habit progress at <date> with <value>
	Then Habit progress has values <LastUpdatedDate> and <LastUpdatedValue> and <LastUpdatedIsOnTarget> and <daysOnTarget>
	Examples: 
	| date       | value | LastUpdatedDate | LastUpdatedIsOnTarget | daysOnTarget |
	| 2020-04-13 | 1     | 2020-04-13      | true                  | 1            |
	

Scenario Outline: Update habit progress with numeric target

Scenario Outline: Update habit progress with rank target

Scenario: After three consecutive days user see congratulations

Scenario: Can't update target not in progress days



Feature: Competences views
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers


Scenario: Query learner competeces list

Scenario: Query learner active habits

Scenario: Query competence active habits

Scenario: Query competence inactive habits

Scenario: Query habit week progress 

Scenario: Query habit history


