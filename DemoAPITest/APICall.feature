Feature: APICall

@ListUsers
Scenario: List users API
	When user sends 'list users' request
	Then validate that users are listed

@SingleUser
Scenario: Single user API
	When user sends 'single user' request
	Then validate that single user is found

@SingleUserNotFound
Scenario: Single user not found API
	When user sends 'single user not found' request
	Then validate that single user is not found

@SingleResourceNotFound
Scenario: Single resource not found API
	When user sends 'single resource not found' request
	Then validate that single resource is not found

@Create
Scenario: Create API
	Given user inputs the name 'Tonyo'
	And user inputs the job 'seller'
	When user sends 'create' post request
	Then validate user is created

@Delete
Scenario: Delete API
	When user sends 'delete' delete request
	Then validate that user is deleted

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