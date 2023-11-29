Feature: LoginAPI

@Login
Scenario: Login successful
	Given the following details
		| Key      | Value              |
		| email    | eve.holt@reqres.in |
		| password | cityslicka         |
	When user uses API endpoint 'login'
	And user sends a POST request
	Then validate correct login response details are returned
	And validate response code is '200'

@Login
Scenario: Login unsuccessful
	Given the following details
		| Key      | Value        |
		| email    | peter@klaven |
	When user uses API endpoint 'login'
	And user sends a POST request
	Then validate response body is correct
		| Key   | Value            |
		| error | Missing password |
	And validate response code is '400'