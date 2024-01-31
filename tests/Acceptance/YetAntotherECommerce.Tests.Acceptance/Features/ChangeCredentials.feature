Feature: Change credentails

Scenario: Change email
	Given customer has registered with credentials
		| Email             | Password    |
		| customer@test.com | super$ecret |
	And has successfully logged in
	When changed email to 'updatedcustomer@test.com'
	Then can sign in using new email

Scenario: Change password
	Given customer has registered with credentials
		| Email             | Password    |
		| customer@test.com | super$ecret |
	And has successfully logged in
	When changed password to 'updated$ecret'
	Then can sign in using new password