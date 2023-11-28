Feature: LoginAPI

@Login
Scenario: Login successful
	Given user uses API endpoint 'login'
	And uses the following details
		| Key      | Value              |
		| email    | eve.holt@reqres.in |
		| password | cityslicka         |
	When user sends a POST request
	Then validate correct login response details are returned
	And validate response code is '200'

@Login
Scenario: Login unsuccessful
	Given user uses API endpoint 'login'
	And uses the following details
		| Key      | Value        |
		| email    | peter@klaven |
	When user sends a POST request
	Then validate error message is correct
		| Key   | Value            |
		| error | Missing password |
	And validate response code is '400'