Feature: UsersAPI

@Users @ListUsers
Scenario: List users
	When user uses API endpoint 'users?page=2'
	And user sends a GET request
	Then validate response body is correct
		| Key         | Value |
		| page        | 2     |
		| per_page    | 6     |
		| total       | 12    |
		| total_pages | 2     |
	And validate that correct users are listed
		| id  | email                      | first_name | last_name | avatar                                   |
		| 7   | michael.lawson@reqres.in   | Michael     | Lawson   | https://reqres.in/img/faces/7-image.jpg  |
		| 8   | lindsay.ferguson@reqres.in | Lindsay     | Ferguson | https://reqres.in/img/faces/8-image.jpg  |
		| 9   | tobias.funke@reqres.in     | Tobias      | Funke    | https://reqres.in/img/faces/9-image.jpg  |
		| 10  | byron.fields@reqres.in     | Byron       | Fields   | https://reqres.in/img/faces/10-image.jpg |
		| 11  | george.edwards@reqres.in   | George      | Edwards  | https://reqres.in/img/faces/11-image.jpg |
		| 12  | rachel.howell@reqres.in    | Rachel      | Howell   | https://reqres.in/img/faces/12-image.jpg |
	And validate response code is '200'

@Users @SingleUser
Scenario: Single user
	When user uses API endpoint 'users/2'
	And user sends a GET request
	Then validate that correct user is returned
		| id | email                  | first_name | last_name | avatar                                  |
		| 2  | janet.weaver@reqres.in | Janet      | Weaver    | https://reqres.in/img/faces/2-image.jpg |
	And validate response code is '200'

@Users @SingleUser
Scenario: Single user not found
	When user uses API endpoint 'users/23'
	And user sends a GET request
	Then validate response code is '404'

@Users @SingleResource
Scenario: Single resource not found
	When user uses API endpoint 'unknown/23'
	And user sends a GET request
	Then validate response code is '404'

@Users @Create
Scenario: Create user
	Given the following details
		| Key  | Value    |
		| name | Morpheus |
		| job  | leader   |
	When user uses API endpoint 'users'
	And user sends a POST request
	Then validate correct create user details are returned
	And validate response code is '201'

@Users @Delete
Scenario: Delete user
	When user uses API endpoint 'users/2'
	And user sends a DELETE request
	Then validate response code is '204'