Feature: DELETERequests

@Delete
Scenario: Delete API
	When user sends 'delete' delete request
	Then validate that user is deleted