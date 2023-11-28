Feature: RegisterAPI

@Register
Scenario: Register successful
	Given user uses API endpoint 'register'
	And uses the following details
		| Key      | Value              |
		| email    | eve.holt@reqres.in |
		| password | pistol             |
	When user sends a POST request
	Then validate correct register response details are returned
	And validate response code is '200'

@Register
Scenario: Register unsuccessful
	Given user uses API endpoint 'register'
	And uses the following details
		| Key      | Value       |
		| email    | sydney@fife |
	When user sends a POST request
	Then validate error message is correct
		| Key   | Value            |
		| error | Missing password |
	And validate response code is '400'