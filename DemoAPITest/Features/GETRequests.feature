Feature: GETRequests

@ListUsers
Scenario: List users API
	When user sends 'list users' request
	Then validate correct page details are returned
		| page | per_page | total | total_pages |
		| 2    | 6        | 12    | 2           |
	Then validate that users are listed
		| id  | email                      | first_name | last_name | avatar                                   |
		| 7   | michael.lawson@reqres.in   | Michael     | Lawson   | https://reqres.in/img/faces/7-image.jpg  |
		| 8   | lindsay.ferguson@reqres.in | Lindsay     | Ferguson | https://reqres.in/img/faces/8-image.jpg  |
		| 9   | tobias.funke@reqres.in     | Tobias      | Funke    | https://reqres.in/img/faces/9-image.jpg  |
		| 10  | byron.fields@reqres.in     | Byron       | Fields   | https://reqres.in/img/faces/10-image.jpg |
		| 11  | george.edwards@reqres.in   | George      | Edwards  | https://reqres.in/img/faces/11-image.jpg |
		| 12  | rachel.howell@reqres.in    | Rachel      | Howell   | https://reqres.in/img/faces/12-image.jpg |

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