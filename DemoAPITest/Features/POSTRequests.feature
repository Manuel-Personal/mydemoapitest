Feature: POSTRequests

@Create
Scenario: Create API
	Given user inputs the name 'Tonyo'
	And user inputs the job 'seller'
	When user sends 'create' post request
	Then validate user is created

@RegisterSuccessful
Scenario: Register successful API
	Given user inputs the email 'eve.holt@reqres.in'
	And user inputs the password 'pistol'
	When user sends 'register-successful' post request
	Then validate user is registered successfully

@RegisterUnsuccessful
Scenario: Register unsuccessful API
	Given user inputs the email 'sydney@fife'
	When user sends 'register-unsuccessful' post request
	Then validate user is not registered due to missing password

@LoginSuccessful
Scenario: Login successful API
	Given user inputs the email 'eve.holt@reqres.in'
	And user inputs the password 'cityslicka'
	When user sends 'login-successful' post request
	Then validate user is logged in successfully

@LoginUnsuccessful
Scenario: Login unsuccessful API
	Given user inputs the email 'peter@klaven'
	When user sends 'login-unsuccessful' post request
	Then validate missing password error is returned