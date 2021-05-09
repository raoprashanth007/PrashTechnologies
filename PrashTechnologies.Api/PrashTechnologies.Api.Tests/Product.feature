Feature: Product
		POST Product API (POST: /api/Product)

@AddProduct
Scenario: Add Product
	Given I creat a new product(<Name>,<CurrentCost>,<Description>)
	And ModelState is correct
	Then the system sgould return<StatusCode>

Examples: 
	| Name         | CurrentCost | Description |
	| Product-Spec | 250.00      | new product | 	