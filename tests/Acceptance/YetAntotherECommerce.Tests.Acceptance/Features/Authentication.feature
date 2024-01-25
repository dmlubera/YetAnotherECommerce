Feature: Authentication

Scenario: Sign up as customer successfully
	When I sign up as customer with following credentials
		| Email             | Password    |
		| customer@test.com | Super$ecret |
	Then customer account is successfully created
	And it is possible to sign in successfully

Scenario: Sign up as admin successfully
	When I sign up as admin with following credentials
		| Email          | Password    |
		| admin@test.com | Super$ecret |
	Then customer account is successfully created
	And it is possible to sign in successfully

Scenario: Sign in with incorrect credentials
	Given customer has registered with credentials
		| Email             | Password    |
		| customer@test.com | super$ecret |
	When trying to sign in with password 'incorrectPassword'
	Then will get error response

Scenario: Sign up with already used email
	Given customer has registered with credentials
		| Email             | Password    |
		| customer@test.com | super$ecret |
	When trying to sign up with email 'customer@test.com'
	Then will get error response