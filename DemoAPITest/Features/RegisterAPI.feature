Feature: RegisterAPI

@Register
Scenario: Register successful
	Given the following details
		| Key      | Value              |
		| email    | eve.holt@reqres.in |
		| password | pistol             |
	When user uses API endpoint 'register'
	And user sends a POST request
	Then validate correct register response details are returned
	And validate response code is '200'

@Register
Scenario: Register unsuccessful
	Given the following details
		| Key      | Value       |
		| email    | sydney@fife |
	When user uses API endpoint 'register'
	And user sends a POST request
	Then validate response body is correct
		| Key   | Value            |
		| error | Missing password |
	And validate response code is '400'