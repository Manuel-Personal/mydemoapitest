Feature: GETRequests

@ListUsers
Scenario: List users API
	When user sends 'list users' request
	Then validate correct page details are returned
		| page | per_page | total | total_pages |
		| 2    | 6        | 12    | 2           |

@SingleUser
Scenario: Single user API
	When user sends 'single user' request
	Then validate that single user is found
		| id | email                  | first_name | last_name | avatar                                  |
		| 2  | janet.weaver@reqres.in | Janet      | Weaver    | https://reqres.in/img/faces/2-image.jpg |

@SingleUserNotFound
Scenario: Single user not found API
	When user sends 'single user not found' request
	Then validate that single user is not found

@SingleResourceNotFound
Scenario: Single resource not found API
	When user sends 'single resource not found' request
	Then validate that single resource is not found