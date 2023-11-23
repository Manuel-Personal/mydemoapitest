Feature: GETRequests

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